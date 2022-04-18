using Microsoft.AspNetCore.Mvc;
using Serilog;
using SerilogTimings;

namespace SerilogExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;
        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Error")]
        public IActionResult LogError(string value)
        {
            try
            {
                var test = value[10];
                return Ok(value);
            }
            catch (Exception ex)
            {
                _logger.LogAppError(ex, "extension-error");
                //Log.Error(ex.ToString());
                return BadRequest();
            }
        }

        [HttpGet("Timer")]
        public IActionResult LogTime(int DocumentId)
        {
            using (var op = Operation.Begin("{DocumentId} is downloading", DocumentId))
            {
                Thread.Sleep(500);
                op.Abandon();
                op.Complete();

                return Ok();
            }
        }

        [HttpPost("Masking")]
        public IActionResult LogMask(User user)
        {
            try
            {
                Login(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                Log.Warning("Login failed {@User}", new User { Username = user.Username, Password = user.Password });
                //_logger.LogAppWarning("Login failed {@User}");
                return BadRequest();
            }
        }

        public class User
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        private void Login(User user)
        {
            throw new NotImplementedException();
        }
    }
}
