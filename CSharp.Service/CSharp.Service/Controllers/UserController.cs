using CSharp.Service.Contracts;
using CSharp.Service.Errors;
using CSharp.Service.Models;
using CSharp.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CSharp.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _pythonServiceUrl;

        public UserController(
            IUserService userService,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            AppDbContext appDbContext
        )
        {
            _userService = userService;
            _pythonServiceUrl = configuration["PythonServiceUrl"]!;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("/users")]
        public async Task<IActionResult> CreateAsync([FromBody] UserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Lastname) ||
                string.IsNullOrWhiteSpace(request.Firstname) ||
                request.Age < 0)
            {
                return BadRequest(new { Message = ErrorMessage.INVALID });
            }

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync(
                $"{_pythonServiceUrl}/api/process",
                new
                {
                    request.Lastname,
                    request.Firstname,
                    source = "C# service"
                });

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode(500, ErrorMessage.SERVER);
            }

            await _userService.CreateAsync(request);

            return Ok();
        }
    }
}
