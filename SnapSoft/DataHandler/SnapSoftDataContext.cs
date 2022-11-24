using Microsoft.EntityFrameworkCore;
using SnapSoft.Models;

namespace SnapSoft.DataHandler
{
    public class SnapSoftDataContext : DbContext
    {
        public DbSet<BaseModel> RequestsAndResponses { get; set; }

        public SnapSoftDataContext(DbContextOptions<SnapSoftDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
    }
}
