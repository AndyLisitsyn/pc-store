using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WEB.Models
{
    public class AuthOptions
    {
        public const string Issuer = "MyServer";
        public const string Audience = "MyClient";
        const string Key = "mysupersecret_secretkey!123";
        public const int Lifetime = 60;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
