using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetMating.Api.Models;

namespace PetMating.Api.Helpers
{
    public class Pagination<T> where T : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IEnumerable<T> Data { get; set; }

        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
        {
            this.Data = data;
            this.Count = count;
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
        }

        public static IReadOnlyList<T> PagedList(IEnumerable<T> ListToQuery, PageSpecsParams pageSpecsParams)
        {
            return ListToQuery.Skip((pageSpecsParams.PageIndex - 1) * pageSpecsParams.PageSize).Take(pageSpecsParams.PageSize).ToList();
        }

        public static async Task<Pagination<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new Pagination<T>(pageIndex, pageSize, count, items);
        }

        // Example of pagination
        // users = users.Skip((pageSpecsParams.PageIndex - 1) * pageSpecsParams.PageSize).Take(pageSpecsParams.PageSize);
    }
}