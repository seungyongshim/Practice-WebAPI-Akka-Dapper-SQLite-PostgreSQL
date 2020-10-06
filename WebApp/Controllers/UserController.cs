using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Core;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            UserService = userService;
            Logger = logger;
        }

        public IUserService UserService { get; }
        public ILogger<UserController> Logger { get; }

        [HttpPut]
        public async Task<IActionResult> PutAsync(User user)
        {
            return Ok(await UserService.PutAsync(user));
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAsync()
        {
            return await UserService.GetAsync();
        }
    }
}