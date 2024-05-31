using DigiTipGreen.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigiTipGreen.API.Data
{
    public class TipDataContext : IdentityDbContext<User>
    {
        public TipDataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "EPerson", NormalizedName = "EPERSON" },
                new IdentityRole { Name = "APerson", NormalizedName = "APERSON" },
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" }
             );
        }
    }
}
