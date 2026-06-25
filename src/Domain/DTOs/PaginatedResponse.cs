namespace NinxERP.Domain.DTOs;
    public class PaginatedResponse<T>
    {
        public List<T> Data { get; set; } = new();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
        public int TotalAtivos { get; set; } = 0;
        public int TotalNormal { get; set; } = 0;
        public int TotalBaixo { get; set; } = 0;
        public int TotalZerado { get; set; } = 0;

        public PaginatedResponse()
        {
            Data = new List<T>();
        }
    }
