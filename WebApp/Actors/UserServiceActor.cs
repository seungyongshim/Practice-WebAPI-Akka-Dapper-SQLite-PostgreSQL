using Akka.Actor;
using Akka.DI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Messages;

namespace WebApp.Actors
{
    public class UserServiceActor : ReceiveActor
    {
        public UserServiceActor()
        {
            Receive<InsertUserMessage>(Handle);
            Receive<GetUserMessage>(Handle);
        }

        private void Handle(GetUserMessage msg)
        {
            var getUserActor = Context.ActorOf(Context.DI().Props<GetUserActor>());
            getUserActor.Forward(msg);
        }

        private void Handle(InsertUserMessage msg)
        {
            var insertUserActor = Context.ActorOf(Context.DI().Props<InsertUserActor>());
            insertUserActor.Forward(msg);
        }
    }
}
