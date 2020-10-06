using Akka.Actor;
using Akka.Event;
using Database.Core;
using Messages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Actors
{
    public class GetUserActor : ReceiveActor
    {
        protected GetUserActor() {}

        public GetUserActor(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Receive<GetUserMessage>(Handle);
        }

        public ILoggingAdapter Logger { get; } = Context.GetLogger();
        public IServiceProvider ServiceProvider { get; }

        private void Handle(GetUserMessage msg)
        {
            using var s = ServiceProvider.CreateScope();
            var UserRepository = s.ServiceProvider.GetService<IUserRepository>();
            var UserGroupRepository = s.ServiceProvider.GetService<IUserGroupRepository>();

            UserRepository.FindAll().Select(x => x.UserGroup = UserGroupRepository.Find(x.USER_GROUP_FK));

            Sender.Tell(UserRepository.FindAll().ToList());
        }
    }
}