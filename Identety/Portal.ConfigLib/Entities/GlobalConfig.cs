using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Xml.Serialization;

namespace Market.ConfigLib.Entities
{
    [XmlRoot]
    public class GlobalConfig
    {


        public string TokenKey { get; set; }

        public string FrontendUrl { get; set; }

        public string PolicyName { get; set; }

        public string ConnectionStringDB { get; set; }

        /// <summary>
        /// для работы сервиса messanger
        /// </summary>
        public string ConnectionsDb { get; set; }

        public AuthOptions AuthOptions { get; set; }
    }

    public class AuthOptions {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }

}
