using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class SchoolSubjects
    {
        public SchoolSubjects()
        {
            SubjectTeaching = new HashSet<SubjectTeaching>();
        }
        [Key]
        public int Id { get; set; }
        private string? _subjects;

        public string? Subjects
        {
            get => _subjects;
            set => _subjects = value?.Trim();
        }
        [JsonIgnore]
        public virtual ICollection<SubjectTeaching> SubjectTeaching { get; set; }
    }
}
