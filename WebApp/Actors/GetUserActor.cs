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
        public GetUserActor(IServiceProvider serviceProvider, IUserRepository userRepository)
        {
            ServiceProvider = serviceProvider;
            UserRepository = userRepository;
            Receive<GetUserMessage>(Handle);
        }

        private void Handle(GetUserMessage msg)
        {
            using (var s = ServiceProvider.CreateScope())
            {
                s.ServiceProvider.GetService<IUnitOfWork<User>>();
                Sender.Tell(UserRepository.FindAll().ToList());
            }
                
        }

        public IServiceProvider ServiceProvider { get; }
        public IUserRepository UserRepository { get; }
    }
}