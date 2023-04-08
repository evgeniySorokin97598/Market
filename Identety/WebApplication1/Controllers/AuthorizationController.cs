using Back.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Market.IdentetyServer.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Back.Controllers
{
    [Route("Home")]
    [ApiController]
    public class AuthorizationController : Controller
    {

        private SignInManager<IdentetyUser> _signInManager;
        private Mediator _mediator;
        private IJwtGenerator _jwtGenerator;
        public AuthorizationController(SignInManager<IdentetyUser> signInManager, Mediator mediator, IJwtGenerator jwtGenerator)
        {

            _signInManager = signInManager;
            _mediator = mediator;
            _jwtGenerator = jwtGenerator;
        }






        ////[Authorize]
        //[NonAction]
        //public ClaimsIdentity GetIdentity()
        //{

        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
        //    var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName



        //    //if (_dataBaseWorker.CheckCreds(loginModel))
        //    //{
        //    //    var claims = new List<Claim>
        //    //    {
        //    //        new Claim(ClaimsIdentity.DefaultNameClaimType, loginModel.key),
        //    //    };
        //    //    ClaimsIdentity claimsIdentity =
        //    //    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);


        //    //    return claimsIdentity;
        //    //}

        //    // если пользователя не найдено
        //    return null;
        //}

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> LoginAsync([FromBody] LoginQuery query)
        {
            return await _mediator.Send(query);
        }

        [Authorize]
        [HttpGet("TestAuth")]
        public IActionResult TestAuth()
        {
            return Ok();
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationEntity user)
        {


            user.UserName = user.Email;
            var result = await _signInManager.UserManager.CreateAsync(user, user.Password);

            if (result.Succeeded)
            {

                //user.Id = identety.Id;



                return Ok(new UserModel() { 
                 Token= _jwtGenerator.CreateToken(user),
                });
            }
            return StatusCode(600, result.Errors);
        }


    }
}
