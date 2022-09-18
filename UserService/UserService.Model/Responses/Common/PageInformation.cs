using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UserService.Model.Responses.Common
{
    public class PageInformation
    {
        [JsonIgnore]
        public int Offset
        {
            get
            {
                return (Page - 1) * PageSize;
            }
            set
            {
                Offset = (Page - 1) * PageSize;
            }
        }
        public int Page { get; set; }
        public int PageSize { get; set; }

    }
}
