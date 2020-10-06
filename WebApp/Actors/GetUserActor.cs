using Akka.Actor;
using Akka.Event;
using Database.Core;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebApp.Messages;

namespace WebApp.Actors
{
    public class GetUserActor : ReceiveActor
    {
        public ILoggingAdapter Logger { get; } = Context.GetLogger();
        public GetUserActor(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Receive<GetUserMessage>(Handle);
        }

        private void Handle(GetUserMessage msg)
        {
            using var s = ServiceProvider.CreateScope();
            var UserRepository = s.ServiceProvider.GetService<IUserRepository>();
            
            Sender.Tell(UserRepository.FindAll().ToList());
        }

        public IServiceProvider ServiceProvider { get; }
    }
}