using Application.ApplicationServices.Maps;
using Application.Common.Interfaces;
using Infrastructure.DataAccess.FilterOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ApplicationServices.IServices
{
    public interface IStudentService : ICrudService<StudentDto>
    {
        Task<IEnumerable<StudentDto>> FindAllStudentsAsync(StudentFilterOptions? filterOptions = null, int pageNumber = 1, int pageSize = int.MaxValue);
    }
}
