using Domain.Models;

namespace Domain.DTOs
{
    public class StudentDTOs
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Department { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTime DateEnrolled { get; set; }
        public DateTime DOB { get; set; }
        public int ClassInSchoolId { get; set; }
    }
    public class StudentGuardianDTOs
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Occupation { get; set; }
        public string Relationship { get; set; }
        public string status { get; set; }
    }
}
