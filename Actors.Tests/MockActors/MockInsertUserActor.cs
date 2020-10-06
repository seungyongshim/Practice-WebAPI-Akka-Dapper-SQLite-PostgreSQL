using Actors;
using Akka.Actor;
using Akka.TestKit;
using Messages;
using System;

namespace Actors.Tests
{
    public class MockInsertUserActor : InsertUserActor
    {
        public MockInsertUserActor(TestProbe testProbe) 
        {
            ReceiveAny(m => testProbe.Forward(m));
        }
    }
}