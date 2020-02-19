using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyCrm.Core.Model;

namespace TinyCrm.Web.Models
{
    public class SearchProductViewModel
    {
        public Core.Model.Options.SearchProductOptions SearchOptions { get; set; }

        public List<Product> ProductList { get; set; }

        public string ErrorText { get; set; }

        public string SuccessText { get; set; }
    }
}
