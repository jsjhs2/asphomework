// Controllers/TestController.cs
using Microsoft.AspNetCore.Mvc;
using SchoolManagementApp.Services;

namespace SchoolManagementApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IUserAuthenticationService _authService;

        public TestController(IUserAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpGet("hash/{password}")]
        public IActionResult GetPasswordHash(string password)
        {
            var hash = _authService.HashPassword(password);
            return Ok(new
            {
                password,
                hash,
                expectedHashFor123456 = "n4bQgYhMfWWaL+qgxVrQFaO/TxsrC4Is0V1sFbDwCg="
            });
        }
    }
}