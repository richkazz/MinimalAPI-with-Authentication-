using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ClassInSchool
    {
        [Key]
        public int Id { get; set; }
        public string? ClassName { get; set; }
    }
}
