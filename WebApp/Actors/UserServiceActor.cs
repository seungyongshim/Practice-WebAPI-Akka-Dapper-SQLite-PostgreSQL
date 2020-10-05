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
        }

        private void Handle(InsertUserMessage msg)
        {
            var insertUserActor = Context.ActorOf(Context.DI().Props<InsertUserActor>());
            insertUserActor.Forward(msg);
        }
    }
}
