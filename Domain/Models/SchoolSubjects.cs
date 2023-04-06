using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class SchoolSubjects
    {
        [Key]
        public int Id { get; set; }
        private string? _subjects;

        public string? Subjects
        {
            get => _subjects;
            set => _subjects = value?.Trim();
        }
    }
}
