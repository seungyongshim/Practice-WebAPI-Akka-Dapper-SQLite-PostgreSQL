using Akka.Actor;
using Akka.DI.Core;
using Akka.DI.Extensions.DependencyInjection;
using Akka.TestKit;
using Akka.TestKit.Xunit2;
using FluentAssertions;
using Messages;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Actors.Tests
{
    public class UserServiceActorSpec : TestKit
    {
        public UserServiceActorSpec()
        {
        }

        [Fact]
        public void GetUserMessage()
        {
            var services = new ServiceCollection();
            services.AddSingleton(sp => CreateTestProbe());
            services.AddTransient<UserServiceActor>();
            services.AddTransient<GetUserActor, MockGetUserActor>();
            var serviceProvider = services.BuildServiceProvider();

            var testProbe = serviceProvider.GetService<TestProbe>();

            Sys.UseServiceProvider(serviceProvider);

            var userServiceActor = Sys.ActorOf(Sys.DI().Props<UserServiceActor>());
            var msg = new GetUserMessage();

            userServiceActor.Tell(msg);

            testProbe.ExpectMsg<GetUserMessage>().Should().BeEquivalentTo(msg);
        }

        [Fact]
        public void InsertUserMessage()
        {
            var services = new ServiceCollection();
            services.AddSingleton(sp => CreateTestProbe());
            services.AddTransient<UserServiceActor>();
            services.AddTransient<InsertUserActor, MockInsertUserActor>();
            var serviceProvider = services.BuildServiceProvider();
            Sys.UseServiceProvider(serviceProvider);

            var testProbe = serviceProvider.GetService<TestProbe>();
            var userServiceActor = Sys.ActorOf(Sys.DI().Props<UserServiceActor>());
            var msg = new InsertUserMessage(default);

            userServiceActor.Tell(msg);

            testProbe.ExpectMsg<InsertUserMessage>().Should().BeEquivalentTo(msg);
        }
    }
}