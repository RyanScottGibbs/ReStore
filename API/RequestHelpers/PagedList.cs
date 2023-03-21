using Microsoft.EntityFrameworkCore;

namespace API.RequestHelpers
{
    public class PagedList<T> : List<T>
    {
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = getCurrentPage(count, pageSize, pageNumber),
                TotalPages = getTotalPages(count, pageSize)
            };
            AddRange(items);
        }
        public MetaData MetaData { get; set; }

        public static async Task<PagedList<T>> ToPagedList(IQueryable<T> query, 
            int pageNumber, int pageSize)
        {            
            var count = await query.CountAsync();

            pageNumber = getCurrentPage(count, pageSize, pageNumber);

            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        private static int getTotalPages(int count, int pageSize)
        {
            return (int)Math.Ceiling(count / (double)pageSize);
        }

        private static int getCurrentPage(int count, int pageSize, int pageNumber)
        {
            var totalPages = getTotalPages(count, pageSize);

            if(pageNumber > totalPages) pageNumber = totalPages;

            return pageNumber;
        }
    }
}