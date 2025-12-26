namespace StudentManagement.Application.Shared
{

    public abstract class PaginationRequest
    {
        public int Page { get; set; } 
        public int PageSize { get; set; }
    }
    public abstract class PaginationResponse
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    }
}
