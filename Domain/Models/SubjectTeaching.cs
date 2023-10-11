using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class SubjectTeaching
    {
        [Key]
        public int Id { get; set; }
        public int SchoolSubjectsId { get; set; }
        public int SchoolTeacherId { get; set; }

        [JsonIgnore]
        public virtual Teacher? Teacher { get; set; }
        public virtual SchoolSubjects? SchoolSubjects { get; set; }
    }
}
