using WebAPI_LKP.Models.Enums;

namespace WebAPI_LKP.DTO
{
    public class ProductToDeliverDTO
    {
        public string Email {  get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public Delivery Delivery { get; set; }
        public double Price { get; set; }
    }
}
