using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel
{
    public class TeacherViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        public string? Qualification { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? NextOfKinName { get; set; }
        [Required]
        public string? NextOfkinNumber { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public DateTime? DateEmployed { get; set; }
        public List<SubjectTeaching>? SubjectTeachingList { get; set; }
    }
}
