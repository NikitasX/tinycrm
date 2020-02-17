using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyCrm.Core.Model;

namespace TinyCrm.Web.Models
{
    public class CreateCustomerViewModel
    {
        public Core.Model.Options.CreateCustomerOptions CreateOptions { get; set; }

        public IEnumerable<Country> CountryList { get; set; }

        public string ErrorText { get; set; }

        public string SuccessText { get; set; }
    }
}
