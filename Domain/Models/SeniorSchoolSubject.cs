using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class SeniorSchoolSubject
    {
        
        [Key]
        public int Id { get; set; }
        public int SubjectId { get; set; }

        public virtual SchoolSubjects? SchoolSubjects { get; set; }
    }
}
