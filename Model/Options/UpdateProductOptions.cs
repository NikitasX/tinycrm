namespace TinyCrm.Model.Options
{
    public class UpdateProductOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? Discount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ProductCategory ProductCategory { get; set; }
    }
}
