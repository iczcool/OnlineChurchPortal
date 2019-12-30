using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineWebPortal.Paging
{
    public class Paging<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int NumOfPages { get; set; }
        public Paging(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            NumOfPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }
        public bool HasPreviousPage
        {
            get{return (PageIndex > 1);}
        }
        public bool HasNextPage
        {
            get{return (PageIndex < NumOfPages);}
        }
        public int TotalPageNum
        {
            get{return NumOfPages;}
        }

        public static Paging<T> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new Paging<T>(items, count, pageIndex, pageSize);
        }
    }
}
