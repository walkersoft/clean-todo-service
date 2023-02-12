using CleanTodo.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CleanTodo.IntegrationTests
{
    public class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.Remove(services.Single(s => s.ServiceType == typeof(DbContextOptions<TodoDbContext>)));
                services.AddDbContext<TodoDbContext>(options =>
                {
                    options.UseSqlServer(GetConnectionString());
                });
            });

            base.ConfigureWebHost(builder);
        }

        private string GetConnectionString()
        {
            return string.Format(
                @"Server=(localdb)\MSSQLLocalDB;Database=TodoTestDB_{0};Trusted_Connection=true",
                Guid.NewGuid().ToString()
            );
        }
    }
}
