using Actors;
using Akka.Actor;
using Akka.Bootstrap.Docker;
using Akka.Configuration;
using Akka.DI.Extensions.DependencyInjection;
using Database.Core;
using Database.SQLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Data;
using System.IO;
using webapi.Services;
using WebApp.Core;
using WebApp.Services;

namespace webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "webapi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                scope.ServiceProvider.GetService<IUserRepository>().Initialize();
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddHostedService<AkkaHostedService>();
            services.AddTransient<UserServiceActor>();
            services.AddTransient<InsertUserActor>();
            services.AddTransient<GetUserActor>();
            services.AddScoped<IDbConnection>(s => new SqliteConnection("Data Source=Application.db;Cache=Shared"));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddSingleton(s =>
            {
                var config = ConfigurationFactory.ParseString(File.ReadAllText("app.conf")).BootstrapFromDocker();
                var sys = ActorSystem.Create("webapi", config);
                return sys.UseServiceProvider(s);
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "webapi", Version = "v1" });
            });
        }
    }
}