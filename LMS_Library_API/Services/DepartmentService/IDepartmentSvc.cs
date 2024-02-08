﻿using LMS_Library_API.Models;

namespace LMS_Library_API.Services.DepartmentService
{
    public interface IDepartmentSvc
    {
        Task<Logger> Create(Department department);
        Task<Logger> Update(Department department);
        Task<Logger> Delete(string departmentId);
        Task<Logger> GetById(string departmentId);
        Task<IEnumerable<Department>> GetAll();
        Task<IEnumerable<Department>> Search(string query);

    }
}
