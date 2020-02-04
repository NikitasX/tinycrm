namespace TinyCrm.Core.Model.Options
{
    public class SearchProductOptions
    {

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal MaxPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal MinPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal MaxDiscount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal MinDiscount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ProductCategory Category { get; set; }
    }
}
