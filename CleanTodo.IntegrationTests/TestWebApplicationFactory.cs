using CleanTodo.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace CleanTodo.Api.Tests
{
    public class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        const string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=TodoTestDB;Trusted_Connection=true";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.Remove(services.Single(s => s.ServiceType == typeof(DbContextOptions<TodoDbContext>)));
                services.AddDbContext<TodoDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });
            });
            
            base.ConfigureWebHost(builder);
        }
    }
}
