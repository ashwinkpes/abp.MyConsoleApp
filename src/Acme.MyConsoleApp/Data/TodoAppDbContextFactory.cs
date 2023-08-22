using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.MyConsoleApp.Data
{
    public class TodoAppDbContextFactory : IDesignTimeDbContextFactory<TodoAppDbContext>
    {
        public TodoAppDbContext CreateDbContext(string[] args)
        {

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<TodoAppDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Server=(LocalDb)\\MSSQLLocalDB;Database=TodoApp;Trusted_Connection=True;TrustServerCertificate=True"));

            return new TodoAppDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
