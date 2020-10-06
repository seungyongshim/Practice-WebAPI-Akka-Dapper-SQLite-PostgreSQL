using Database.Core;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Database.SQLite.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDbConnection>(s => new SqliteConnection("Data Source=file:memdb1?mode=memory&cache=shared"));
            services.AddTransient<IUnitOfWork,UnitOfWork>();
            services.AddTransient<UserRepository>();
        }
    }
}
