using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Utilities.Constants;

namespace Data.EF
{
    public class UOrderDbContextFactory : IDesignTimeDbContextFactory<UOrderDbContext>
    {
        public UOrderDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString(SystemConstants.MainConnectionString);

            var optionsBuilder = new DbContextOptionsBuilder<UOrderDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new UOrderDbContext(optionsBuilder.Options);
        }
    }
}