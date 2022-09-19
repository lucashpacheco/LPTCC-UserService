using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

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
        }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Page { get; set; }

        [Range(10, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int PageSize { get; set; }

    }
}
