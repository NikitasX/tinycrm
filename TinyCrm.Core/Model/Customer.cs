using System;
using System.Collections.Generic;

namespace TinyCrm.Core.Model
{
    public class Customer
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VatNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? Status { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Country Country { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<Order> Orders { get; set; }
 
        /// <summary>
        /// 
        /// </summary>
        public ICollection<ContactPerson> ContactPeople { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Customer()
        {
            Created = DateTimeOffset.Now;
            Orders = new List<Order>();
            ContactPeople = new List<ContactPerson>();
        }

    }
}
