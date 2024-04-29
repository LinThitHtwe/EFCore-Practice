namespace EFCorePractice.StudentManagement.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, NotFoundException innerException) : base(message, innerException) { }
    }
}
