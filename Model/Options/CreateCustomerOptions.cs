using System;

namespace TinyCrm.Model.Options
{
    public class CreateCustomerOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VatNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Status { get; set; }
    }
}
