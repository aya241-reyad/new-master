using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taisser.Manager
{
    
    public class PaggingManger<T> : List<T>
    {
        
        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int PageIndex { get; set; }

        public PaggingManger(List<T> items, int pageSize, int pageIndex)
        {
            this.PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(items.Count() / (double)pageSize);
            this.AddRange(items);
        }
        public bool PreviousPage
        {
            get
            {
                return (PageSize > 1);
            }
        }

        public bool NextPage
        {

            get
            {
                return (PageSize < TotalPages);
            }

        }
        public static PaggingManger<T> Create(IEnumerable<T> Items, int pageSize, int pageIndex)
        {
            var Count = Items.Count();
            var Result = Items.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaggingManger<T>(Result, pageSize, pageIndex);

        }
        public static (PaggingManger<T>,int,int) CreateWithPageNum(IEnumerable<T> Items, int pageSize, int pageIndex)
        {
            var Result = Items.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            int PageNum = (int)Math.Ceiling(Items.Count() / (double)pageSize);

            return (new PaggingManger<T>(Result, pageSize, pageIndex), PageNum, Items.Count());

        }

    }

}
