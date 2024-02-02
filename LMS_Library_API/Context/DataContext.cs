using Microsoft.EntityFrameworkCore;
using LMS_Library_API.Models;
using LMS_Library_API.Models.Notification;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models.RoleAccess;
using LMS_Library_API.Models.Exams;

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
        public DbSet<ExamRecentViews> ExamRecentViews { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Role_Permissions> Role_Permissions { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<QuestionBanks> QuestionBanks { get; set; }
        public DbSet<QB_Answer_Essay> QB_Answers_Essay { get; set; }
        public DbSet<QB_Answer_MC> QB_Answers_MC { get; set; }
        public DbSet<Question_Exam> Question_Exam { get; set; }

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
            modelBuilder.Entity<ExamRecentViews>().ToTable("ExamRecentViews");
            modelBuilder.Entity<Permissions>().ToTable("Permissions");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Role_Permissions>().ToTable("Role_Permissions");
            modelBuilder.Entity<Exam>().ToTable("Exam");
            modelBuilder.Entity<QuestionBanks>().ToTable("QuestionBanks");
            modelBuilder.Entity<QB_Answer_Essay>().ToTable("QB_Answer_Essay");
            modelBuilder.Entity<QB_Answer_MC>().ToTable("QB_Answer_MC");
            modelBuilder.Entity<Question_Exam>().ToTable("Question_Exam");


            modelBuilder.Entity<NotificationSetting>()
            .HasKey(ns => new { ns.UserId, ns.FeaturesId });

            modelBuilder.Entity<Role_Permissions>()
           .HasKey(rp => new { rp.RoleId, rp.PermissionsId });

            modelBuilder.Entity<Question_Exam>()
          .HasKey(qe => new { qe.ExamId, qe.QuestionId });

            modelBuilder.Entity<ExamRecentViews>()
        .HasKey(erv => new { erv.UserId, erv.ExamId });

        }
        
    }
}
