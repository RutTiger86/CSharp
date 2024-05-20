using System.Text.Json.Serialization;

namespace CSharp.Authorization.OAuth.Models.Google
{
    public class GoogleOAuth
    {
        [JsonPropertyName("access_token")]
        public string accessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int expiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string refreshToken { get; set; }


        [JsonPropertyName("scope")]
        public string scope { get; set; }

        [JsonPropertyName("token_type")]
        public string tokenType { get; set; }

        [JsonPropertyName("id_token")]
        public string idToken { get; set; }
    }
}
