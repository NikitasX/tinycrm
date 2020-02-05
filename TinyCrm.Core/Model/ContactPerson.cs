using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCrm.Core.Model
{
    public class ContactPerson
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Firstname { get; set; }        
        
        /// <summary>
        /// 
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ContactPersonPositions Position { get; set; }
    }
}
