using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class PagedListServiceResponse<T>
    {
        public T? Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }//Total number of items in CommandLine List, divided by PageSize
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
