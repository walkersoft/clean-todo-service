using CleanTodo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanTodo.IntegrationTests.Infrastructure
{
    public sealed class TestDatabaseFixture
    {
        const string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=TodoTestDB;Trusted_Connection=true";

        private static readonly object locked = new();
        private static bool dbInitialized;

        public TestDatabaseFixture()
        {
            lock (locked)
            {
                if (!dbInitialized)
                {
                    using var context = CreateContext();
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    context.Database.Migrate();
                    dbInitialized = true;
                }
            }
        }

        public TodoDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<TodoDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            return new TodoDbContext(options);
        }
    }
}
