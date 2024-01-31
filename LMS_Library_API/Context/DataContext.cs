using Microsoft.EntityFrameworkCore;
using LMS_Library_API.Models;
using LMS_Library_API.Models.Notification;
using LMS_Library_API.Models.AboutUser;

namespace LMS_Library_API.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<SystemInfomation> SystemInfomation { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationFeatures> NotificationFeatures { get; set; }
        public DbSet<NotificationSetting> NotificationSetting { get; set; }
        public DbSet<QnALikes> QnALikes { get; set; }
        public DbSet<Help> Helps { get; set; }
        public DbSet<PrivateFile> PrivateFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<SystemInfomation>().ToTable("SystemInfomation");
            modelBuilder.Entity<Notification>().ToTable("Notification");
            modelBuilder.Entity<NotificationFeatures>().ToTable("NotificationFeatures");
            modelBuilder.Entity<NotificationSetting>().ToTable("NotificationSetting");
            modelBuilder.Entity<QnALikes>().ToTable("QnALikes");
            modelBuilder.Entity<Help>().ToTable("Help");
            modelBuilder.Entity<PrivateFile>().ToTable("PrivateFile");


            modelBuilder.Entity<NotificationSetting>()
            .HasKey(ns => new { ns.UserId, ns.FeaturesId });

        }
        
    }
}
