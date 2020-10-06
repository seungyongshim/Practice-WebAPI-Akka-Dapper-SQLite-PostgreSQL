using Actors;
using Akka.Actor;
using Akka.Bootstrap.Docker;
using Akka.Configuration;
using Akka.DI.Extensions.DependencyInjection;
using Database.Core;
using Database.Factories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
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
                scope.ServiceProvider.GetRequiredService<IUserRepository>().Initialize();
                scope.ServiceProvider.GetRequiredService<IUserGroupRepository>().Initialize();
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DatabaseOptions>(Configuration.GetSection(DatabaseOptions.Database));
            services.AddLogging();
            services.AddHostedService<AkkaHostedService>();
            services.AddTransient<UserServiceActor>();
            services.AddTransient<InsertUserActor>();
            services.AddTransient<GetUserActor>();
            services.AddSingleton<DbConnectionFactory>();
            services.AddSingleton<UserRepositoryFactory>();
            services.AddSingleton<UserGroupRepositoryFactory>();
            services.AddScoped(x => x.GetService<DbConnectionFactory>().Make(x));
            services.AddScoped(x => x.GetService<UserRepositoryFactory>().Make(x));
            services.AddScoped(x => x.GetService<UserGroupRepositoryFactory>().Make(x));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
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