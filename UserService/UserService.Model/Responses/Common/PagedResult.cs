using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Model.Responses.Common
{
    public class PagedResult<T>
    {
        public int TotalItems { get; set; }
        public List<T> Result { get; set; }
    }
}
