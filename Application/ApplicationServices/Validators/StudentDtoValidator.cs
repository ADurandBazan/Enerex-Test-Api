using Application.ApplicationServices.Maps;
using FluentValidation;

namespace Application.ApplicationServices.Validators
{
    public class StudentDtoValidator : AbstractValidator<StudentDto>
    {

        public StudentDtoValidator()
        {


            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage(m => "The field name can't be empty")
                .MaximumLength(250)
                .WithMessage(m => "The field name only allow a max to 250 charaters");
            
            RuleFor(p => p.Education)
                .NotEmpty()
                .WithMessage(m => "The field education can't be empty")
                .MaximumLength(250)
                .WithMessage(m => "The field education only allow a max to 250 charaters");
            
            RuleFor(p => p.Gender)
                .NotEmpty()
                .WithMessage(m => "The field gender can't be empty")
                .Must(IsGenderValid)
                .WithMessage("The gender is invalid.Only values 'F' or 'M' are allowed.");

            RuleFor(x => x.Age)
               .GreaterThan(15)
               .WithMessage("Age must be greater than 15.")
               .LessThan(80)
               .WithMessage("Age must be less than 80.");
           
            RuleFor(x => x.AcademicYear)
               .GreaterThan(0)
                .WithMessage("Academic year must be betwen 1 and 5.")
               .LessThan(6)
               .WithMessage("Academic year must be betwen 1 and 5.");



        }

        private bool IsGenderValid(StudentDto studentDto, string newValue)
        {
           return newValue.ToLower().Equals("f") || newValue.ToLower().Equals("m");
        }

    }
}
