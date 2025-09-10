using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Testing.Controllers
{
    [PublicAPI]
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        public const string ExceptionMessage = "ExceptionMessage";

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Test successful");
        }

        [HttpGet("exception")]
        public IActionResult ThrowException()
        {
            throw new Exception(ExceptionMessage);
        }
    }
}