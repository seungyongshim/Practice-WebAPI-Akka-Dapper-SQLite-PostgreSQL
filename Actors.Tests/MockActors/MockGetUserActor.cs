using Actors;
using Akka.Actor;
using Akka.TestKit;
using Messages;
using System;

namespace Actors.Tests
{
    public class MockGetUserActor : GetUserActor
    {
        public MockGetUserActor(TestProbe testProbe) 
        {
            ReceiveAny(m => testProbe.Forward(m));
        }
    }
}