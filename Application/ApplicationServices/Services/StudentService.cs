using Application.ApplicationServices.IServices;
using Application.ApplicationServices.Maps;
using AutoMapper;
using Domain.Entities;
using Infrastructure.DataAccess.FilterOptions;
using Infrastructure.DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ApplicationServices.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<StudentDto> CreateAsync(StudentDto studentDto, CancellationToken cancellationToken = default)
        {
            var student = _mapper.Map<Student>(studentDto);
            student.Id = 0;
            await _studentRepository.SaveAsync(student);

            return _mapper.Map<StudentDto>(student);
        }

        public async Task DeleteAsync(int studentId, CancellationToken cancellationToken = default)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student is not null)
            {
                await _studentRepository.DeleteAsync(student);
            }
            else
                throw new Exception($"Student with the id {studentId} not found");
        }

        public async Task<IEnumerable<StudentDto>> FindAllStudentsAsync(StudentFilterOptions? filterOptions = null, int pageNumber = 1, int pageSize = int.MaxValue)
        {
            var students = await _studentRepository.FindAllStudentsAsync(filterOptions, pageNumber, pageSize);
            return _mapper.Map<List<StudentDto>>(students.Items.ToList());
        }

        public async Task<StudentDto> GetByIdAsync(int studentId, CancellationToken cancellationToken = default)
        {
            var student = await _studentRepository.GetByIdAsync(studentId, cancellationToken);
            
            if(student is not null)
            return _mapper.Map<StudentDto>(student);
            
            else
                throw new Exception($"Student with the id {studentId} not found");
        }

        public async Task<StudentDto?> UpdateAsync<T>(T id, StudentDto studentDto, CancellationToken cancellationToken = default)
        {
            var student = await _studentRepository.GetByIdAsync(id, cancellationToken);

            if (student is not null) 
            {
                _mapper.Map(studentDto, student);
                await _studentRepository.UpdateAsync(student, cancellationToken);
                return _mapper.Map<StudentDto>(student);
            }
            return null;
        }

    }
}
