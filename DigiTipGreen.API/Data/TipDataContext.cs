using Microsoft.EntityFrameworkCore;

namespace DigiTipGreen.API.Data
{
    public class TipDataContext : DbContext
    {
        public TipDataContext(DbContextOptions options) : base(options)
        {
        }
    }
}
