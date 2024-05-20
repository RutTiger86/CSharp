using CSharp.Authorization.OAuth.Enums;
using CSharp.Authorization.OAuth.Helpers;
using CSharp.Authorization.OAuth.Models.Google;
using CSharp.Authorization.OAuth.Models.Response;
using CSharp.Authorization.OAuth.ViewModels;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace CSharp.Authorization.OAuth.Models.Logins
{
    public interface ILoginService
    {
        public Task<BaseResponse<GoogleUserInfo>> TryGoogleLogin();
    }

    public class LoginService: BaseModel, ILoginService
    {
        private readonly OAuthInfo oAuthInfo = OAuthHelper.GetOAuthInfo();
        private readonly string userInfoEndpoint = "https://www.googleapis.com/oauth2/v3/userinfo";
        private readonly string SCOPE = "openid https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile";


        public async Task<BaseResponse<GoogleUserInfo>> TryGoogleLogin()
        {
            try
            {
                // Generates state and PKCE values.
                string state = CryptographyHelper.randomDataBase64url(32);
                string code_verifier = CryptographyHelper.randomDataBase64url(32);
                string code_challenge = CryptographyHelper.base64urlencodeNoPadding(CryptographyHelper.sha256(code_verifier));
                const string code_challenge_method = "S256";

                // Creates a redirect URI using an available port on the loopback address.
                string redirectURI = $"http://{IPAddress.Loopback}:{GetRandomUnusedPort()}/";

                // Creates an HttpListener to listen for requests on that redirect URI.           
                HttpListener http = new();
                http.Prefixes.Add(redirectURI);
                http.Start();

                // Creates the OAuth 2.0 authorization request.
                string authorizationRequest = $"{oAuthInfo.AuthUri}?response_type=code&scope={SCOPE}&redirect_uri={Uri.EscapeDataString(redirectURI)}&client_id={oAuthInfo.ClientId}&state={state}&code_challenge={code_challenge}&code_challenge_method={code_challenge_method}";

                // Opens request in the browser.
                Process.Start(new ProcessStartInfo { FileName = authorizationRequest, UseShellExecute = true });

                // Waits for the OAuth authorization response.
                HttpListenerContext context = await http.GetContextAsync();


                // Sends an HTTP response to the browser.
                HttpListenerResponse response = context.Response;
                string responseString = "<html><head><meta http-equiv='refresh' content='10;url=https://google.com'></head><body>Please return to the app.</body></html>";
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                Stream responseOutput = response.OutputStream;
                Task responseTask = responseOutput.WriteAsync(buffer, 0, buffer.Length).ContinueWith((task) =>
                {
                    responseOutput.Close();
                    http.Stop();
                });

                // Checks for errors.
                if (context.Request.QueryString.Get("error") != null)
                {
                    return new BaseResponse<GoogleUserInfo>() { Result = false, ErrorCode = (int)ErrorCode.OAUTH_GOOGLE, ErrorMessage = context.Request.QueryString.Get("error") };
                }
                if (context.Request.QueryString.Get("code") == null || context.Request.QueryString.Get("state") == null)
                {
                    return new BaseResponse<GoogleUserInfo>() { Result = false, ErrorCode = (int)ErrorCode.OAUTH_GOOGLE, ErrorMessage = context.Request.QueryString.ToString() };
                }

                return await InvalidCode_GetUSer(context, state, redirectURI, code_verifier);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GoogleUserInfo>() { Result = false, ErrorCode = (int)ErrorCode.EC_EX, ErrorMessage = ex.ToString() };
            }
        }

        private int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }
        
        private async Task<BaseResponse<GoogleUserInfo>> InvalidCode_GetUSer(HttpListenerContext context, string state, string redirectURI, string code_verifier)
        {
            try
            {
                // extracts the code
                string? code = context.Request.QueryString.Get("code");
                string? incoming_state = context.Request.QueryString.Get("state");

                // Compares the receieved state to the expected value, to ensure that
                // this app made the request which resulted in authorization.
                if (incoming_state != state)
                {
                    return new BaseResponse<GoogleUserInfo>() { Result = false, ErrorCode = (int)ErrorCode.OAUTH_GOOGLE_INVALID_STATE, ErrorMessage = incoming_state };
                }

                // Starts the code exchange at the Token Endpoint.
                BaseResponse<GoogleOAuth> CodeExchange = await PerformCodeExchange(oAuthInfo, code, code_verifier, redirectURI);


                if (CodeExchange.Result)
                {
                    BaseResponse<GoogleUserInfo> userInfo = await GetUserInfo(CodeExchange.Data.accessToken, CodeExchange.Data.tokenType);

                    if (userInfo.Result)
                    {
                        userInfo.Data.refreshToken = CodeExchange.Data.refreshToken;
                    }

                    return userInfo;
                }
                else
                {
                    return new BaseResponse<GoogleUserInfo>()
                    {
                        Result = false,
                        ErrorCode = CodeExchange.ErrorCode,
                        ErrorMessage = CodeExchange.ErrorMessage,
                        Data = default
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<GoogleUserInfo>() { Result = false, ErrorCode = (int)ErrorCode.EC_EX, ErrorMessage = ex.ToString() };
            }
        }

        private async Task<BaseResponse<GoogleOAuth>> PerformCodeExchange(OAuthInfo GoogleO, string code, string code_verifier, string redirectURI)
        {
            try
            {
                // Builds the Token request
                string tokenRequestBody = $"code={code}&redirect_uri={System.Uri.EscapeDataString(redirectURI)}&client_id={GoogleO.ClientId}&code_verifier={code_verifier}&client_secret={GoogleO.ClientSecret}&scope={SCOPE}&grant_type=authorization_code";

                StringContent content = new(tokenRequestBody, Encoding.UTF8, "application/x-www-form-urlencoded");

                // Performs the authorization code exchange.
                HttpClientHandler handler = new();
                handler.AllowAutoRedirect = true;
                HttpClient client = new(handler);

                HttpResponseMessage response = await client.PostAsync(GoogleO.TokenUri, content);
                string responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new BaseResponse<GoogleOAuth>() { Result = false, ErrorCode = (int)ErrorCode.EC_HTTP, ErrorMessage = $"[{response.StatusCode}] {responseString}" };
                }
                else
                {
                    // Sets the Authentication header of our HTTP client using the acquired access token.              
                    BaseResponse<GoogleOAuth> baseResponse = new()
                    {
                        Result = true,
                        Data = JsonSerializer.Deserialize<GoogleOAuth>(responseString)
                    };

                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<GoogleOAuth>() { Result = false, ErrorCode = (int)ErrorCode.EC_EX, ErrorMessage = ex.ToString() };
            }
        }

        private async Task<BaseResponse<GoogleUserInfo>> GetUserInfo(string access_token, string token_type = "Bearer")
        {
            try
            {
                // Performs the authorization code exchange.
                HttpClientHandler handler = new();
                handler.AllowAutoRedirect = true;
                HttpClient client = new(handler);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token_type, access_token);

                // Makes a call to the Userinfo endpoint, and prints the results.
                HttpResponseMessage response = client.GetAsync(userInfoEndpoint).Result;
                string responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new BaseResponse<GoogleUserInfo>() { Result = false, ErrorCode = (int)ErrorCode.EC_HTTP, ErrorMessage = $"[{response.StatusCode}] {responseString}" };
                }
                else
                {
                    // Sets the Authentication header of our HTTP client using the acquired access token.              
                    BaseResponse<GoogleUserInfo> baseResponse = new()
                    {
                        Result = true,
                        Data = JsonSerializer.Deserialize<GoogleUserInfo>(responseString)
                    };
                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<GoogleUserInfo>() { Result = false, ErrorCode = (int)ErrorCode.EC_EX, ErrorMessage = ex.ToString() };
            }
        }

        public async Task<BaseResponse<GoogleOAuth>> RefrashAccessToken(OAuthInfo GoogleO, string refresh_token)
        {
            try
            {
                // Builds the Token request
                string tokenRequestBody = $"client_id={GoogleO.ClientId}&client_secret={GoogleO.ClientSecret}&refresh_token={refresh_token}&grant_type=refresh_token";

                StringContent content = new(tokenRequestBody, Encoding.UTF8, "application/x-www-form-urlencoded");

                // Performs the authorization code exchange.
                HttpClientHandler handler = new();
                handler.AllowAutoRedirect = true;
                HttpClient client = new(handler);

                HttpResponseMessage response = await client.PostAsync(GoogleO.TokenUri, content);
                string responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new BaseResponse<GoogleOAuth>() { Result = false, ErrorCode = (int)ErrorCode.EC_HTTP, ErrorMessage = $"[{response.StatusCode}] {responseString}" };
                }
                else
                {
                    // Sets the Authentication header of our HTTP client using the acquired access token.              
                    BaseResponse<GoogleOAuth> baseResponse = new()
                    {
                        Result = true,
                        Data = JsonSerializer.Deserialize<GoogleOAuth>(responseString)
                    };

                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<GoogleOAuth>() { Result = false, ErrorCode = (int)ErrorCode.EC_EX, ErrorMessage = ex.ToString() };
            }
        }

    }
}
