using Akka.Actor;
using Database.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebApp.Messages;

namespace WebApp.Actors
{
    public class InsertUserActor : ReceiveActor
    {
        public InsertUserActor(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Receive<InsertUserMessage>(Handle);
        }

        private void Handle(InsertUserMessage msg)
        {
            using (var s = ServiceProvider.CreateScope())
            {
                var uow = s.ServiceProvider.GetService<IUnitOfWork>();
                var userRepository = s.ServiceProvider.GetService<IUserRepository>();
                userRepository.Insert(msg.User);
            }
        }

        public IServiceProvider ServiceProvider { get; }
    }
}