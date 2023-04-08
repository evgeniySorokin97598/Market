using MediatR;

namespace Back.Auth
{
    public class LoginQuery : IRequest<UserModel>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
