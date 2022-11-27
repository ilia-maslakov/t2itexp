using Microsoft.EntityFrameworkCore;

namespace t2itexp.Data.EF
{
    public partial class DBphoneContext : DbContext
    {
        public DbSet<Phone> Phone { get; set; }

        public DBphoneContext(DbContextOptions<DBphoneContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phone>().HasData(
                new Phone { Id = 1, Code = 1, Value = "121" },
                new Phone { Id = 2, Code = 2, Value = "122" },
                new Phone { Id = 3, Code = 3, Value = "123" },
                new Phone { Id = 4, Code = 4, Value = "124" },
                new Phone { Id = 5, Code = 5, Value = "125" }
            );

            modelBuilder.HasAnnotation("testphone", "0.01");

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.ToTable("phones");
            });
        }
    }
}
