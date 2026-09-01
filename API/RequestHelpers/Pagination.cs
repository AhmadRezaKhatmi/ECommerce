namespace API.RequestHelpers
{
    public class Pagination<T>(int pageIndex , int pageSize , int count , IReadOnlyList<T> data)
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }

        public IReadOnlyList<T> Data { get; set; }=data;
    }
}
