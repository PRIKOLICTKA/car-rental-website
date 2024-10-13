using Microsoft.EntityFrameworkCore;
using webcrp10._2.Model;

namespace webcrp10._2.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Zayavki> Zayavki { get; set; }
        public DbSet<Arenda> Arenda { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("USERS")
                .HasKey(u => u.ID);

            modelBuilder.Entity<Zayavki>()
                .ToTable("ZAYAVKI")
                .HasKey(z => z.ID);

            modelBuilder.Entity<Arenda>() // Добавлено для конфигурации таблицы АРЕНДА
                .ToTable("АРЕНДА")
                .HasKey(a => a.ID);
        }
    }
}




