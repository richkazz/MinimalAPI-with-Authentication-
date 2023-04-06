using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class CurrentGradingSystem
    {
        [Key]
        public int Id { get; set; }
        public int GradingSystem { get; set; }
    }
}
