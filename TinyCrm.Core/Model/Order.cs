using System.Collections.Generic;

namespace TinyCrm.Core.Model
{
    public class Order
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DeliveryAdress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<OrderProduct> Products { get; set; }
        
        public Order()
        {
            Products = new List<OrderProduct>();
        }
    }
}
