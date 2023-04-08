 
using Market.IdentetyServer.Entities;

namespace Back.Auth
{
    public interface IJwtGenerator
    {
        string CreateToken(IdentetyUser user);
    }
}
