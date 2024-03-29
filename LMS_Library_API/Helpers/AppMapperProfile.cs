﻿using AutoMapper;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using LMS_Library_API.Models.AboutSubject;
using LMS_Library_API.Models.AboutUser;
using LMS_Library_API.Models.Exams;
using LMS_Library_API.Models.Notification;
using LMS_Library_API.Models.RoleAccess;
using LMS_Library_API.Models.StudentNotification;
using LMS_Library_API.ModelsDTO;
using System.Net;

namespace LMS_Library_API.Helpers
{
    public class AppMapperProfile:Profile
    {
        public AppMapperProfile()
        {
            CreateMap<RoleDTO, Role>();
            CreateMap<Role_PermissionsDTO, Role_Permissions>();
            CreateMap<UserDTO, User>();
            CreateMap<HelpDTO, Help>();
            CreateMap<PrivateFileDTO, PrivateFile>();
            CreateMap<PUTPrivateFileDTO, PrivateFile>();
            CreateMap<SystemInfomationDTO, SystemInfomation>();
            CreateMap<NotificationDTO, Notification>();
            CreateMap<NotificationSettingDTO, NotificationSetting>();
            CreateMap<SubjectDTO, Subject>();
            CreateMap<PartDTO, Part>();
            CreateMap<LessonDTO, Lesson>();
            CreateMap<DocumentDTO, Document>();
            CreateMap<ExamDTO, Exam>();
            CreateMap<AutoExamDTO, Exam>();
            CreateMap<QuestionBankES_DTO, QuestionBanks>();
            CreateMap<QuestionBankMC_DTO, QuestionBanks>();
            CreateMap<QB_Answer_ES_DTO, QB_Answer_Essay>();
            CreateMap<QB_Answer_MC_DTO, QB_Answer_MC>();
            CreateMap<Question_ExamDTO, Question_Exam>();
            CreateMap<ExamRecentViewDTO, ExamRecentViews>();
            CreateMap<ClassDTO, Class>();
            CreateMap<StudentDTO, Student>();
            CreateMap<CustomInfoOfSubjectDTO, CustomInfoOfSubject>();
            CreateMap<StuNotificationSettingDTO, StudentNotificationSetting>();
            CreateMap<StudyTimeDTO, StudyTime>();
            CreateMap<StudyHistoryDTO, StudyHistory>();
            CreateMap<ClassSubjectDTO, ClassSubject>();
            CreateMap<StudentSubjectDTO, StudentSubject>();
            CreateMap<TeacherClassDTO, TeacherClass>();
            CreateMap<SubjectNotificationDTO, SubjectNotification>();
            CreateMap<NotificationClassStudentDTO, NotificationClassStudent>();
            CreateMap<LessonAccessDTO, LessonAccess>();
            CreateMap<DocumentAccessDTO, DocumentAccess>();
            CreateMap<LessonAnswerDTO, LessonAnswer>();
            CreateMap<LessonQuestionDTO, LessonQuestion>();
            CreateMap<AnswerLikeDTO, AnswerLike>();
            CreateMap<QuestionLikeDTO, QuestionLike>();
            CreateMap<StudentAnswerLikeDTO, StudentAnswerLike>();
            CreateMap<StudentQuestionLikeDTO, StudentQuestionLike>();
            CreateMap<MC_ExamDTO, Exam>();
            CreateMap<MC_QuestionExamDTO, Question_Exam>();
            CreateMap<MC_QuestionBankDTO, QuestionBanks>();
            CreateMap<Essay_ExamDTO, Exam>();
            CreateMap<Essay_QuestionExamDTO, Question_Exam>();
            CreateMap<Essay_QuestionBankDTO, QuestionBanks>();
        }
    }
}
