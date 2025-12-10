using GLeadGenerator.Integration.Contract.JsonPlaceholder;
using GLeadGenerator.Integration.Contract.JsonPlaceholder.Users;
using Microsoft.AspNetCore.Mvc;

namespace GLeadGenerator.Integration.Api.Controllers
{
    [Route("api/json-placeholder")]
    [ApiController]
    public class JsonPlaceholderController : Controller
    {
        private readonly IJsonPlaceholderService _jsonPlaceholderService;

        public JsonPlaceholderController(IJsonPlaceholderService jsonPlaceholderService)
        {
            _jsonPlaceholderService = jsonPlaceholderService;
        }

        [HttpGet("users")]
        public async Task<ActionResult<UserDto?>> GetUserAsync([FromQuery] GetUsersRequest request)
        {
            var user = (await _jsonPlaceholderService.GetUsersAsync(request))
                .Users?
                .FirstOrDefault();

            return user;
        }
    }
}
