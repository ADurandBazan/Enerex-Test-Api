using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Student : BaseEntity
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string Education { get; set; }
        public int AcademicYear { get; set; }
    }
}
