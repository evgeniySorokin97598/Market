using IdentityModel;
using Market.Entities.Dto;
using Market.Entities.Requests;
using Market.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Market.Controllers
{
    [Route("Comments")]
    public class CommentsController : Controller
    {
        private readonly ICommentsRepository _repository;
        private LoggerLib.Interfaces.ILogger _Logger;
        public CommentsController(IDataBaseManager manager, LoggerLib.Interfaces.ILogger logger)
        {
            _repository = manager.CommentsRepository;
            _Logger = logger;
        }

        [Authorize]
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] AddCommentRequest request)
        {
            try
            {
                await _repository.AddAsync(request, GetUserInfo());
                return Ok();
            }
            catch (Exception ex)
            {
                _Logger.Error($"Ошибка при добавлении комментария {ex.Message}");
                return StatusCode(500);
            }

        }

        private UserInfo GetUserInfo()
        {
            return new UserInfo()
            {
                Id = User.FindFirstValue(JwtClaimTypes.ClientId),
                Username = User.FindFirstValue("username"),
            };

        }
    }
}
