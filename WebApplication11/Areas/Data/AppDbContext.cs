using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Identity.Models;

namespace Identity.Areas.Data
{
    public class AppDbContext:IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions options):base(options) 
        {
            
        }
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Item> Items { get; set; }
    }
}
