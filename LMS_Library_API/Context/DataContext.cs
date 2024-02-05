﻿using Microsoft.EntityFrameworkCore;
using LMS_Library_API.Models;
using LMS_Library_API.Models.Notification;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models.RoleAccess;
using LMS_Library_API.Models.Exams;
using LMS_Library_API.Models.AboutSubject;
using LMS_Library_API.Models.StudentNotification;
using LMS_Library_API.Models.AboutStudent;

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
        public DbSet<Department> Departments { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonQuestion> LessonQuestions { get; set; }
        public DbSet<LessonAnswer> LessonAnswers { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<CustomInfoOfSubject> CustomInfoOfSubjects { get; set; }
        public DbSet<SubjectNotification> SubjectNotifications { get; set; }
        public DbSet<NotificationClassStudent> NotificationClassStudents { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentQnALikes> StudentQnALikes { get; set; }
        public DbSet<StudentNotification> StudentNotifications { get; set; }
        public DbSet<StudentNotificationSetting> StudentNotificationSetting { get; set; }
        public DbSet<StudentNotificationFeatures> StudentNotificationFeatures { get; set; }



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
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Subject>().ToTable("Subject");
            modelBuilder.Entity<Part>().ToTable("Part");
            modelBuilder.Entity<Lesson>().ToTable("Lesson");
            modelBuilder.Entity<LessonQuestion>().ToTable("LessonQuestion");
            modelBuilder.Entity<LessonAnswer>().ToTable("LessonAnswer");
            modelBuilder.Entity<Document>().ToTable("Document");
            modelBuilder.Entity<CustomInfoOfSubject>().ToTable("CustomInfoOfSubject");
            modelBuilder.Entity<SubjectNotification>().ToTable("SubjectNotification");
            modelBuilder.Entity<NotificationClassStudent>().ToTable("NotificationClassStudent");
            modelBuilder.Entity<Class>().ToTable("Class");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<StudentNotificationFeatures>().ToTable("StudentNotificationFeatures");
            modelBuilder.Entity<StudentNotification>().ToTable("StudentNotification");
            modelBuilder.Entity<StudentQnALikes>().ToTable("StudentQnALikes");
            modelBuilder.Entity<StudentNotificationSetting>().ToTable("StudentNotificationSetting");


            modelBuilder.Entity<NotificationSetting>()
            .HasKey(ns => new { ns.UserId, ns.FeaturesId });

            modelBuilder.Entity<Role_Permissions>()
            .HasKey(rp => new { rp.RoleId, rp.PermissionsId });

            modelBuilder.Entity<Question_Exam>()
            .HasKey(qe => new { qe.ExamId, qe.QuestionId });

            modelBuilder.Entity<ExamRecentViews>()
            .HasKey(erv => new { erv.UserId, erv.ExamId });

            modelBuilder.Entity<NotificationClassStudent>()
            .HasKey(ncs => new { ncs.subjectNotificationId, ncs.classId, ncs.studentId });

            modelBuilder.Entity<StudentNotificationSetting>()
            .HasKey(sns => new { sns.studentId, sns.featuresId });
        }
        
    }
}
