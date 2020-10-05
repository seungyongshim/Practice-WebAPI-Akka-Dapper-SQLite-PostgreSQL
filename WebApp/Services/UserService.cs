using Akka.Actor;
using Akka.DI.Core;
using Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Core;
using WebApp.Messages;
using FluentAssertions.Extensions;

namespace WebApp.Services
{
    public class UserService : IUserService
    {
        public UserService(ActorSystem actorSystem, ILogger<UserService> logger)
        {
            ActorSystem = actorSystem;
            Logger = logger;
        }

        public ActorSystem ActorSystem { get; }
        public ILogger<UserService> Logger { get; }

        public async Task<IEnumerable<User>> GetAsync()
        {
            var userServiceActor = await ActorSystem.ActorSelection("/user/UserServiceActor").ResolveOne(1.Seconds());
            return await userServiceActor?.Ask<IEnumerable<User>>(new GetUserMessage(), 1.Seconds());
        }

        public async Task PutAsync(User user)
        {
            var userServiceActor = await ActorSystem.ActorSelection("/user/UserServiceActor").ResolveOne(1.Seconds());
            await userServiceActor?.Ask<OkMessage>(new InsertUserMessage(user), 1.Seconds());
        }
    }
}
