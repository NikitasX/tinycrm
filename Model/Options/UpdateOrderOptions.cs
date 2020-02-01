using System.Collections.Generic;

namespace TinyCrm.Model.Options
{
    public class UpdateOrderOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string DeliveryAdress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Product> Products;
    }
}
