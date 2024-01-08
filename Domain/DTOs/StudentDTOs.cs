using Domain.Models;

namespace Domain.DTOs
{
    public class StudentDTOs
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateEnrolled { get; set; }
        public DateTime DOB { get; set; }
        public int ClassInSchoolId { get; set; }
        public StudentGuardianDTOs StudentGuardianDTOs { get; set; } = new StudentGuardianDTOs();
    }
    public class StudentGuardianDTOs
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Occupation { get; set; } = string.Empty;
        public string Relationship { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
