using Microsoft.EntityFrameworkCore;
using Database.Models;

namespace Database.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        
        
        public DbSet<Folder> Folders { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка иерархии папок (у папки может быть родительская папка)
            modelBuilder.Entity<Folder>()
                .HasOne<Folder>()
                .WithMany()
                .HasForeignKey(f => f.ParentId)
                .IsRequired(false);
                //.OnDelete(DeleteBehavior.Restrict);
                
            // Настройка связи "Папка может содержать много продуктов"
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Folder)
                .WithMany(f => f.Products)
                .HasForeignKey(p => p.FolderId)
                .IsRequired(false);
                //.OnDelete(DeleteBehavior.Cascade);
        }
    }
}