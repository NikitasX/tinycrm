using System;

namespace TinyCrm.Core.Model.Options
{
    public class SearchCustomerOptions
    { 
        /// <summary>
        /// 
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VatNumber { get; set; }

        /// <summary>
        /// 
        /// 
        /// </summary>
        public DateTimeOffset? CreatedFrom { get; set; }

        /// <summary>
        /// 
        /// 
        /// </summary>
        public DateTimeOffset? CreatedTo { get; set; }
    }
}
