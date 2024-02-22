using LMS_Library_API.Context;
using LMS_Library_API.Enums;
using LMS_Library_API.Models;
using LMS_Library_API.ModelsDTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace LMS_Library_API.Services.SubjectService
{
    public class SubjectSvc: ISubjectSvc
    {
        private readonly DataContext _context;
        public SubjectSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<Logger> Create(Subject subject)
        {
            try
            {
                foreach (var item in await GetCheck())
                {
                    if (string.Equals(subject.Id, item.Id,StringComparison.OrdinalIgnoreCase))
                    {
                        return new Logger()
                        {
                            status = TaskStatus.Faulted,
                            message = "Đã tồn tại mã môn học này",
                            data = subject
                        };
                    }
                }

                subject.Id = subject.Id.ToUpper();

                _context.Subjects.Add(subject);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thêm thành công",
                    data = subject
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

        public async Task<Logger> Delete(string subjectId)
        {
            try
            {
                var existSubject = await _context.Subjects.FindAsync(subjectId);

                if (existSubject == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng muốn xoá"
                    };
                }

                _context.Remove(existSubject);
                await _context.SaveChangesAsync();
                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Xoá thành công",
                    data = existSubject

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
                var respone = await _context.Subjects.Include(_ => _.Parts)
                    .ThenInclude(_ => _.Lessons)
                    .ThenInclude(_ => _.Documents)
                    .ToListAsync();


                IEnumerable<GETSubjectDTO> result = respone.Select(_ => new GETSubjectDTO
                {
                    Id = _.Id,
                    Name = _.Name,
                    SubmissionDate = _.SubmissionDate,
                    Description = _.Description,
                    CountDocument = _.Parts.SelectMany(p => p.Lessons)
                                           .SelectMany(l => l.Documents)
                                           .Count(),
                    CountDocumentApproved = _.Parts.SelectMany(p => p.Lessons)
                                           .SelectMany(l => l.Documents)
                                           .Count(d => d.status == Status.Approved),
                    Teacher = _.User
                });


                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thành công",
                    listData = new List<object>() { result }
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

        public async Task<Logger> GetById(string subjectId)
        {
            try
            {
                var respone = await _context.Subjects.Include(_ => _.Parts)
                    .ThenInclude(_ => _.Lessons)
                    .ThenInclude(_ => _.Documents)
                    .FirstOrDefaultAsync(_ => _.Id == subjectId);

                if (respone == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần tìm"
                    };
                }

                GETSubjectDTO result = new GETSubjectDTO
                {
                    Id = respone.Id,
                    Name = respone.Name,
                    SubmissionDate = respone.SubmissionDate,
                    Description = respone.Description,
                    CountDocument = respone.Parts.SelectMany(p => p.Lessons)
                                           .SelectMany(l => l.Documents)
                                           .Count(),
                    CountDocumentApproved = respone.Parts.SelectMany(p => p.Lessons)
                                           .SelectMany(l => l.Documents)
                                           .Count(d => d.status == Status.Approved),
                    Teacher = respone.User
                };


                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thành công",
                    data = result
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

        private async Task<IEnumerable<Subject>> GetCheck()
        {
            try
            {
                var respone = await _context.Subjects.ToListAsync();
                return respone;
            }
            catch (Exception)
            {
                return new List<Subject>();
            }
        }

        public async Task<Logger> Search(string query)
        {
            try
            {
                var respone = await _context.Subjects.Include(_ => _.Parts)
                    .ThenInclude(_ => _.Lessons)
                    .ThenInclude(_ => _.Documents)
                    .Where(_ => _.Name.Contains(query) || _.Id.Contains(query))
                    .ToListAsync();


                IEnumerable<GETSubjectDTO> result = respone.Select(_ => new GETSubjectDTO
                {
                    Id = _.Id,
                    Name = _.Name,
                    SubmissionDate = _.SubmissionDate,
                    Description = _.Description,
                    CountDocument = _.Parts.SelectMany(p => p.Lessons)
                                           .SelectMany(l => l.Documents)
                                           .Count(),
                    CountDocumentApproved = _.Parts.SelectMany(p => p.Lessons)
                                           .SelectMany(l => l.Documents)
                                           .Count(d => d.status == Status.Approved),
                    Teacher = _.User
                });


                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Thành công",
                    listData = new List<object>() { result }
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

        public async Task<Logger> Update(Subject subject)
        {
            try
            {

                Subject existSubject = await _context.Subjects.FindAsync(subject.Id);

                if (existSubject == null)
                {
                    return new Logger()
                    {
                        status = TaskStatus.Faulted,
                        message = "Không tìm thấy đối tượng cần cập nhật"
                    };
                }

                existSubject.Name = subject.Name;
                existSubject.SubmissionDate = DateTime.Now;
                existSubject.TeacherId = subject.TeacherId;
                existSubject.DepartmentId = subject.DepartmentId;
                existSubject.Description = subject.Description;

                await _context.SaveChangesAsync();

                return new Logger()
                {
                    status = TaskStatus.RanToCompletion,
                    message = "Cập nhật thành công",
                    data = existSubject
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
