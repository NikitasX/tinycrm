using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyCrm.Core.Model;

namespace TinyCrm.Web.Models
{
    public class SearchCustomerViewModel
    {
        public Core.Model.Options.SearchCustomerOptions SearchOptions { get; set; }

        public IEnumerable<Country> CountryList { get; set; }

        public IEnumerable<Customer> CustomerList { get; set; }

        public string ErrorText { get; set; }

        public string SuccessText { get; set; }
    }
}
