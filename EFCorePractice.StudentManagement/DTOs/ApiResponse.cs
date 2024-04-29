namespace EFCorePractice.StudentManagement.DTOs
{
    public class ApiResponse
    {
        public string Status => IsSuccess ? "Success" : "Fail";
        public bool IsSuccess { get; set; }
        public object Data { get; set; }
    }

    public class ApiResponseSuccess<T>
    {
        public string Status =>  "Success";
        public bool IsSuccess => true;
        public T Data { get; set; }
    }

    public class ApiResponseFail<T>
    {
        public string Status => "Fail";
        public bool IsSuccess => false;
        public T Data { get; set; }
    }
}
