namespace Domain.Models
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string? StatusPharase { get; set; }
        public List<string> Errors { get; } = new();
        public DateTime TimeStamp { get; set; }
    }
}
