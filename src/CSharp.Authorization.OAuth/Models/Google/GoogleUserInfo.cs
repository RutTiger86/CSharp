namespace CSharp.Authorization.OAuth.Models.Google
{
    public class GoogleUserInfo
    {
        public string? sub { get; set; }
        public string? name { get; set; }

        public string? givenName { get; set; }

        public string? familyName { get; set; }

        public string? picture { get; set; }

        public string? email { get; set; }

        public bool emailVerified { get; set; }

        public string? locale { get; set; }

        public string? refreshToken { get; set; }
    }
}
