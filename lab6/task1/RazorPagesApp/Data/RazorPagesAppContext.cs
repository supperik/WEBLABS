using Microsoft.EntityFrameworkCore;

namespace RazorPagesApp.Data
{
    public class RazorPagesAppContext : DbContext
    {
        public RazorPagesAppContext(DbContextOptions<RazorPagesAppContext> options)
            : base(options)
        {
        }

        public DbSet<RazorPagesApp.Models.Movie> Movie { get; set; } = default!;
    }
}