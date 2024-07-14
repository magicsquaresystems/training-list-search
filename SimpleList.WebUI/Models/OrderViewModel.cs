namespace SimpleList.WebUI.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string ProductBrand { get; set; }
        // Add other properties as needed

        public string ProductDescription { get; set; }
        public decimal ProductCost { get; set; }
        public string ProductCode { get; set; }
    }
}
