using Domain.Models;

namespace Domain.ViewModel
{
    public class TeacherViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? Qualification { get; set; }
        public string? PhoneNumber { get; set; }
        public string? NextOfKinName { get; set; }
        public string? NextOfkinNumber { get; set; }
        public string? Address { get; set; }
        public DateTime? DateEmployed { get; set; }
        public List<SubjectTeaching>? SubjectTeachingList { get; set; }
    }
}
