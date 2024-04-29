namespace EFCorePractice.StudentManagement.DTOs
{
    public class PaginationResponse
    {
        public int CurrentPageNo { get; set; }
        public int TotalPage {  get; set; }
        public bool HasNextPage => CurrentPageNo < TotalPage;
        public object PaginatedData { get; set; }
    }
}
