using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webapi.Services;
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
            await UserService.PutAsync(user);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAsync()
        {
            return await UserService.GetAsync();
        }
    }
}
