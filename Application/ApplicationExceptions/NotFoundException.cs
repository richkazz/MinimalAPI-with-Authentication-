namespace Application.ApplicationExceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException() { }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public NotFoundException(string modelName,int Id) : base($"{Id} not found for {modelName}")
        {
        }

    }
}
