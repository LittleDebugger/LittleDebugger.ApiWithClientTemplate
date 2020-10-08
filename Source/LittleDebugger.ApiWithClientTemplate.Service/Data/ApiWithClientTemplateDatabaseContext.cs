using LittleDebugger.ApiWithClientTemplate.Contract.Models;
using Microsoft.EntityFrameworkCore;

namespace LittleDebugger.ApiWithClientTemplate.Service.Models
{
    public class ApiWithClientTemplateDatabaseContext : DbContext
    {
        public ApiWithClientTemplateDatabaseContext (DbContextOptions<ApiWithClientTemplateDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<ExampleModel> Example { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExampleModel>()
                .HasKey(m => m.Id);        
        }
    }
}
