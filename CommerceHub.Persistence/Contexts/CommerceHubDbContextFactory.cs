using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommerceHub.Persistence.Contexts
{
    public class CommerceHubDbContextFactory : IDesignTimeDbContextFactory<CommerceHubDbContext>
    {
        public CommerceHubDbContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();

            // 1. If running from the API folder, basePath is already correct.
            // 2. If running from Solution root, append the API folder name.
            if (!File.Exists(Path.Combine(basePath, "appsettings.json")))
            {
                if (File.Exists(Path.Combine(basePath, "CommerceHub.API", "appsettings.json")))
                {
                    basePath = Path.Combine(basePath, "CommerceHub.API");
                }
                // 3. If running from the Persistence folder, navigate up one level then into the API folder.
                else if (File.Exists(Path.Combine(basePath, "..", "CommerceHub.API", "appsettings.json")))
                {
                    basePath = Path.Combine(basePath, "..", "CommerceHub.API");
                }
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<CommerceHubDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new CommerceHubDbContext(builder.Options);
        }
    }
}
