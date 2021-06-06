using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Savana.Common
{
    public class ApplicationContext<T> : DbContext where T : DbContext
    {
        public ApplicationContext(DbContextOptions<T> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}