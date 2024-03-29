﻿using LMS_Library_API.Context;
using LMS_Library_API.Models;
using LMS_Library_API.Models.AboutStudent;
using Microsoft.EntityFrameworkCore;
using System;

namespace LMS_Library_API.Services.ServiceAboutStudent.StudyHistoryService
{
    public class StudyHistorySvc : IStudyHistorySvc
    {
        private readonly DataContext _context;
        public StudyHistorySvc(DataContext context)
        {
            _context = context;
        }
        public async Task<Logger> Create(StudyHistory studyHistory)
        {
            try
            {
                _context.StudyHistories.Add(studyHistory);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = studyHistory
                };
            }
            catch (Exception ex)
            {
                return new Logger()
                {
                    status = TaskStatus.Faulted,
                    message = ex.Message,

                };
            }
        }

        public async Task<Logger> Delete(string studentId, int lessonId)
        {
            try
            {
                var existHistory = await _context.StudyHistories.FirstOrDefaultAsync(_ => _.LessonId == lessonId && _.StudentId == Guid.Parse(studentId));

                if (existHistory == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existHistory);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = existHistory

                };
            }
            catch (Exception ex)
            {
                return new Logger()
                {
                    status = TaskStatus.Faulted,
                    message = ex.Message,
                };
            }
        }

        public async Task<Logger> GetAll()
        {
            try
            {
                var respone = await _context.StudyHistories.ToListAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thành công",
                    listData = new List<object>() { respone }
                };
            }
            catch (Exception ex)
            {
                return new Logger()
                {
                    status = TaskStatus.Faulted,
                    message = ex.Message,
                };
            }
        }

        public async Task<Logger> GetById(string studentId, int lessonId)
        {
            try
            {
                var existHistory = await _context.StudyHistories.Include(_ => _.Lesson).FirstOrDefaultAsync(_ => _.LessonId == lessonId && _.StudentId == Guid.Parse(studentId));

                if (existHistory == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần tìm"
                    };
                }

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thành công",
                    data = existHistory
                };
            }
            catch (Exception ex)
            {
                return new Logger()
                {
                    status = TaskStatus.Faulted,
                    message = ex.Message,
                };
            }
        }

        public async Task<Logger> GetByStudentId(string studentId)
        {
            try
            {
                var studentHistory = await _context.StudyHistories.Include(_ => _.Lesson).Where(_ => _.StudentId == Guid.Parse(studentId)).OrderByDescending(_ => _.DateUpdate).ToListAsync();

                if (studentHistory == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy danh sách đối tượng cần tìm"
                    };
                }

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thành công",
                    listData = studentHistory
                };
            }
            catch (Exception ex)
            {
                return new Logger()
                {
                    status = TaskStatus.Faulted,
                    message = ex.Message,
                };
            }
        }

        public async Task<Logger> Update(StudyHistory studyHistory)
        {
            try
            {
                var existHistory = await _context.StudyHistories.FirstOrDefaultAsync(_ => _.LessonId == studyHistory.LessonId && _.StudentId == studyHistory.StudentId);

                if (existHistory == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existHistory.StudentId = studyHistory.StudentId;
                existHistory.LessonId = studyHistory.LessonId;
                existHistory.WatchMinutes = studyHistory.WatchMinutes;
                existHistory.DateUpdate = studyHistory.DateUpdate;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = existHistory
                };
            }
            catch (Exception ex)
            {
                return new Logger()
                {
                    status = TaskStatus.Faulted,
                    message = ex.Message,
                };
            }
        }
    }
}
