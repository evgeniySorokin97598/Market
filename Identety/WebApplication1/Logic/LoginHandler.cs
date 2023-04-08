using MediatR;
using Microsoft.AspNetCore.Identity;
using Market.IdentetyServer.Entities;

namespace Back.Auth
{
    public class LoginHandler : IRequestHandler<LoginQuery, UserModel>
    {

        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentetyUser> _userManager;

        private readonly SignInManager<IdentetyUser> _signInManager;
        IJwtGenerator _jwtGenerator;

        public LoginHandler(Microsoft.AspNetCore.Identity.UserManager<IdentetyUser> userManager,
                                   SignInManager<IdentetyUser> signInManager, IJwtGenerator jwtGenerator)

        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;

        }

        public async Task<UserModel> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                //throw new RestException(HttpStatusCode.Forbidden);
                return null;
            }



            IdentetyUser identety = new IdentetyUser();
            identety.UserName = request.Username;
            
            identety.PasswordHash = _signInManager.UserManager.PasswordHasher.HashPassword(identety, request.Password);

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password,false);
            //AQAAAAIAAYagAAAAEKQyuYuwPvx1xP16JBFaaVkYKRw4tCnH9vvL37+F6tpk+19RtIKKwYN0Sto7Okf/UA==
            //AQAAAAIAAYagAAAAEDulGy6TrrX3J+beYvyu/sfmIPr9tDamR9g4A1Gcwg3eb7W/TeFJFzsu6sjrKB3zgg==
            if (result.Succeeded)
            {
                return new UserModel
                {

                    Token = _jwtGenerator.CreateToken(user),
                    //UserName = user.UserName,
                };
            }
            throw new Exception(result.ToString());
             
            //throw new RestException(HttpStatusCode.Unauthorized);
        }
    }
}

