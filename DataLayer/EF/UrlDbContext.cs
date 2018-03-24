using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.EF
{
    public class UrlDbContext : DbContext
    {
        public DbSet<UserUrl> Urls { get; set; }
        public UrlDbContext(DbContextOptions<UrlDbContext> dbContextOptions) : base(dbContextOptions) { }
    }
}
