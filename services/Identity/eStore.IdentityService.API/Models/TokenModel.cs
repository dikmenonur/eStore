using eStore.IdentityService.Core.Entity;

namespace eStore.IdentityService.API.Models
{
    public class TokenModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }

        public TokenModel()
        {

        }

        public TokenModel ConvertTokenModel(TokenEntity tokenEntity)
        {
            TokenModel tokenModel = new TokenModel();
            tokenModel.Token = tokenEntity.Token;
            tokenModel.Expiration = tokenEntity.Expiration;
            tokenModel.RefreshToken = tokenEntity.RefreshToken;
            return tokenModel;
        }
    }
}
