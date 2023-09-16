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
        public DbSet<Setting> Settings { get; set; }
        public DbSet<SettingAccount> SettingsAccounts { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext>options) : base(options)
        {
            if(Database.IsRelational())
                Database.Migrate();
            else
                Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SettingAccount>(e =>
            {
                e.HasKey(x => new { x.Account_Id, x.Setting_Id }); 
                e.HasOne(x => x.Setting).WithMany(x => x.SettingAccounts).HasForeignKey(x=>x.Setting_Id);
                e.HasOne(x => x.Account).WithMany(x => x.AccountSettings).HasForeignKey(x => x.Account_Id);
            });
            builder.Entity<Setting>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.Key).IsUnique();
                e.Property(x => x.Key).HasColumnType("varchar").HasMaxLength(128);
                e.Property(x=>x.Title).HasColumnType("varchar").HasMaxLength(128);
                e.Property(x => x.Description).HasColumnType("varchar(max)");
            });
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
