using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Assessment.Models
{
    public class BookQueryParameters
    {
        public string Title { get; set; }
        public decimal? MinPrice { get; set; }

        public string Author { get; set; }
    }
}
