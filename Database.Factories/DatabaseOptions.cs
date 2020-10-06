using Database.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Factories
{
    public class DatabaseOptions
    {
        public const string Database = "Database";
        public string ConnectionString { get; set; }
        public DatabaseType DatabaseType { get; set; }
    }
}
