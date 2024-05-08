using Application.ApplicationServices.IServices;
using Application.ApplicationServices.Maps;
using Application.ApplicationServices.Validators;
using Infrastructure.DataAccess.FilterOptions;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    /// <summary>
    /// StudentController class for handling student-related requests
    /// </summary>
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly StudentDtoValidator _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentController"/> class
        /// </summary>
        /// <param name="studentService">The student service for handling student-related operations</param>
        /// <param name="validator">The validator for validating student DTOs</param>
        public StudentController(IStudentService studentService, StudentDtoValidator validator)
        {
            _studentService = studentService;
            _validator = validator;
        }

        /// <summary>
        /// Gets all students with optional filtering and pagination
        /// </summary>
        /// <param name="request">The filtering options for the students</param>
        /// <param name="pageNumber">The page number to retrieve</param>
        /// <param name="pageSize">The number of students to retrieve per page</param>
        /// <returns>A list of students with pagination information</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] StudentFilterOptions? request = null, int pageNumber = 1, int pageSize = int.MaxValue)
        {
            var students = await _studentService.FindAllStudentsAsync(request, pageNumber, pageSize);
           
            return Ok(new
            {
                success = true,
                students
            });
        }

        /// <summary>
        /// Creates a new student
        /// </summary>
        /// <param name="studentDto">The student DTO to create</param>
        /// <returns>The created student</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentDto studentDto)
        {
            var result = _validator.Validate(studentDto);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
          
            var student = await _studentService.CreateAsync(studentDto);
            if (student != null)
                return Ok(new
                {
                    success = true,
                    message = "Student created",
                    student
                });

            return StatusCode(500);

        }

        /// <summary>
        /// Updates an existing student
        /// </summary>
        /// <param name="id">The ID of the student to update</param>
        /// <param name="studentDto">The updated student DTO</param>
        /// <returns>The updated student</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentDto studentDto)
        {
            var result = _validator.Validate(studentDto);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var student = await _studentService.UpdateAsync(id, studentDto);
            if (student != null)
                return Ok(new
                {
                    success = true,
                    message = "Student updated",
                    student
                });

            return NotFound(new { success = false, message = "Student not found" });
        }

        /// <summary>
        /// Deletes a student
        /// </summary>
        /// <param name="id">The ID of the student to delete</param>
        /// <returns>A message indicating success</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentService.DeleteAsync(id);

            return Ok(new { success = true, message = "Student deleted" });
        }
    }
}
