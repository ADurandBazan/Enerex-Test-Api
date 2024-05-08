using Application.ApplicationServices.IServices;
using Application.ApplicationServices.Maps;
using AutoMapper;
using Domain.Entities;
using Infrastructure.DataAccess.FilterOptions;
using Infrastructure.DataAccess.IRepositories;

namespace Application.ApplicationServices.Services
{
    /// <summary>
    /// Implementation of the IStudentService interface
    /// </summary>
    public class StudentService : IStudentService
    {

        #region Fields
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the StudentService class
        /// </summary>
        /// <param name="studentRepository">Repository for accessing student data</param>
        /// <param name="mapper">Mapper for converting between entity and DTO objects</param>
        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new student asynchronously
        /// </summary>
        /// <param name="studentDto">Student data to be created</param>
        /// <param name="cancellationToken">Cancellation token for the operation (optional)</param>
        /// <returns>A task that represents the asynchronous operation, containing the created student DTO</returns>
        public async Task<StudentDto> CreateAsync(StudentDto studentDto, CancellationToken cancellationToken = default)
        {
            var student = _mapper.Map<Student>(studentDto);
            student.Id = 0;
            await _studentRepository.SaveAsync(student);

            return _mapper.Map<StudentDto>(student);
        }

        /// <summary>
        /// Deletes a student asynchronously
        /// </summary>
        /// <param name="studentId">ID of the student to be deleted</param>
        /// <param name="cancellationToken">Cancellation token for the operation (optional)</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task DeleteAsync(int studentId, CancellationToken cancellationToken = default)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student is not null)
            {
                await _studentRepository.DeleteAsync(student);
            }
            else
            {
                throw new Exception($"Student with the id {studentId} not found");
            }
        }

        /// <summary>
        /// Finds all students asynchronously based on the provided filter options and pagination settings
        /// </summary>
        /// <param name="filterOptions">Optional filter options to apply to the search</param>
        /// <param name="pageNumber">Optional page number to retrieve (defaults to 1)</param>
        /// <param name="pageSize">Optional page size to retrieve (defaults to int.MaxValue)</param>
        /// <returns>A task that represents the asynchronous operation, containing a list of student DTOs</returns>
        public async Task<IEnumerable<StudentDto>> FindAllStudentsAsync(StudentFilterOptions? filterOptions = null, int pageNumber = 1, int pageSize = int.MaxValue)
        {
            var students = await _studentRepository.FindAllStudentsAsync(filterOptions, pageNumber, pageSize);
            return _mapper.Map<List<StudentDto>>(students.Items.ToList());
        }

        /// <summary>
        /// Retrieves a student by ID asynchronously
        /// </summary>
        /// <param name="studentId">ID of the student to be retrieved</param>
        /// <param name="cancellationToken">Cancellation token for the operation (optional)</param>
        /// <returns>A task that represents the asynchronous operation, containing the retrieved student DTO</returns>
        public async Task<StudentDto> GetByIdAsync(int studentId, CancellationToken cancellationToken = default)
        {
            var student = await _studentRepository.GetByIdAsync(studentId, cancellationToken);

            if (student is not null)
            {
                return _mapper.Map<StudentDto>(student);
            }
            else
            {
                throw new Exception($"Student with the id {studentId} not found");
            }
        }

        /// <summary>
        /// Updates a student asynchronously
        /// </summary>
        /// <typeparam name="T">Type of the student ID</typeparam>
        /// <param name="id">ID of the student to be updated</param>
        /// <param name="studentDto">Updated student data</param>
        /// <param name="cancellationToken">Cancellation token for the operation (optional)</param>
        /// <returns>A task that represents the asynchronous operation, containing the updated student DTO</returns>
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
        #endregion
    }
}
