using Market.Entities.Requests;
using Market.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [Route("Comments")]
    public class CommentsController : Controller
    {
        private readonly ICommentsRepository _repository;
        private LoggerLib.Interfaces.ILogger _Logger;
        public CommentsController(IDataBaseManager manager,LoggerLib.Interfaces.ILogger logger)
        {
            _repository = manager.CommentsRepository;
            _Logger = logger;
        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] AddCommentRequest request)
        {
            try
            {
                await _repository.AddAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                _Logger.Error($"Ошибка при добавлении комментария {ex.Message}");
                return StatusCode(500);
            }

        }
    }
}
