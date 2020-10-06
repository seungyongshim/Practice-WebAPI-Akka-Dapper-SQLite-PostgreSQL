using Akka.Actor;
using Akka.DI.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Petabridge.Cmd.Cluster;
using Petabridge.Cmd.Host;
using Petabridge.Cmd.Remote;
using System.Threading;
using System.Threading.Tasks;
using Actors;

namespace webapi.Services
{
    /// <summary>
    /// AkkaSystem의 수명 주기를 활동을 정의합니다.
    /// </summary>
    public class AkkaHostedService : IHostedService
    {
        public AkkaHostedService(ActorSystem actorSystem, ILogger<AkkaHostedService> logger)
        {
            Sys = actorSystem;
            Logger = logger;
        }

        public ActorSystem Sys { get; }
        public ILogger<AkkaHostedService> Logger { get; }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("StartAsync");

            var pbm = PetabridgeCmd.Get(Sys);
            pbm.RegisterCommandPalette(ClusterCommands.Instance);
            pbm.RegisterCommandPalette(RemoteCommands.Instance);
            pbm.Start();

            Sys.ActorOf(Sys.DI().Props<UserServiceActor>(), "UserServiceActor");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return CoordinatedShutdown.Get(Sys).Run(CoordinatedShutdown.ClrExitReason.Instance);
        }
    }
}