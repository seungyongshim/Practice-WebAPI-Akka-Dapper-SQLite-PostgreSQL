using Akka.Actor;
using Database.Core;
using System;
using WebApp.Messages;

namespace WebApp.Actors
{
    public class InsertUserActor : ReceiveActor
    {
        public InsertUserActor(IUserRepository userRepository)
        {
            UserRepository = userRepository;

            Receive<InsertUserMessage>(Handle);
        }

        private void Handle(InsertUserMessage msg)
        {
            UserRepository.Insert(msg.User);
            Sender.Tell(new OkMessage { });
        }

        public IUserRepository UserRepository { get; }
    }
}