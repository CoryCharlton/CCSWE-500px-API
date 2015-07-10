namespace CCSWE.FiveHundredPx
{
    public class OAuthToken
    {
        #region Constructor
        public OAuthToken()
        {
            
        }

        public OAuthToken(string token, string secret) : this(token, secret, null)
        {
            
        }

        public OAuthToken(string token, string secret, string verifier)
        {
            Token = token;
            Secret = secret;
            Verifier = verifier;
        }
        #endregion

        #region Public Properties
        public string Token { get; set; }

        public string Secret { get; set; }

        public string Verifier { get; set; }
        #endregion
    }
}
