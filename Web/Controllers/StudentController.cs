using Application.ApplicationServices.IServices;
using Application.ApplicationServices.Maps;
using Application.ApplicationServices.Validators;
using Infrastructure.DataAccess.FilterOptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly StudentDtoValidator _validator;

        public StudentController(IStudentService studentService, StudentDtoValidator validator)
        {
            _studentService = studentService;
            _validator = validator;
        }

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentService.DeleteAsync(id);

            return Ok(new { success = true, message = "Student deleted" });
        }
    }
}
