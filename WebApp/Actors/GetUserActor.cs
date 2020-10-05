using Akka.Actor;
using Akka.Event;
using Database.Core;
using System.Linq;
using WebApp.Messages;

namespace WebApp.Actors
{
    public class GetUserActor : ReceiveActor
    {
        public ILoggingAdapter Logger { get; } = Context.GetLogger();
        public GetUserActor(IUserRepository userRepository)
        {
            UserRepository = userRepository;
            Receive<GetUserMessage>(Handle);
        }

        private void Handle(GetUserMessage msg)
        {
            Logger.Debug("GetUserMessage Start");
            Sender.Tell(UserRepository.FindAll().ToList());
        }

        public IUserRepository UserRepository { get; }
    }
}