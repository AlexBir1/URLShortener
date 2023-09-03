using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.DataAccessLayer.Entities;

namespace URLShortener.DataAccessLayer.DBContext
{
    public class AppDBContext : IdentityDbContext<Account>
    {
        public DbSet<ShortURL> ShortURLs { get; set; }
        public DbSet<ShortURLInfo> ShortURLInfos { get; set; }
        public DbSet<AboutContent> AboutContent { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext>options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AboutContent>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).HasColumnType("int");
                e.Property(x => x.Content).HasColumnType("varchar(max)");
            });
            builder.Entity<ShortURL>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).HasColumnType("int").ValueGeneratedOnAdd();
                e.Property(x => x.Url).HasColumnType("varchar(max)");
                e.Property(x => x.Origin).HasColumnType("varchar(max)");
                e.HasOne(url => url.Info)
                .WithOne(urlInfo => urlInfo.ShortURL)
                .HasForeignKey<ShortURLInfo>(fkey => fkey.URL_Id);
            });
            builder.Entity<ShortURLInfo>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).HasColumnType("int").ValueGeneratedOnAdd();
                e.Property(x => x.CreatedBy).HasColumnType("varchar(max)");
            });
            base.OnModelCreating(builder);
        }
    }
}
