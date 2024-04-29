namespace EFCorePractice.StudentManagement.Exceptions
{
    public class DataAlreadyExistsException : Exception
    {
        public DataAlreadyExistsException() { }
        public DataAlreadyExistsException(string message) : base(message) { }
        public DataAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
    }

}
