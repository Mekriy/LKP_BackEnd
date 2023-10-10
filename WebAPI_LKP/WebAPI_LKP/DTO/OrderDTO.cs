namespace WebAPI_LKP.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
        public bool isDelivered { get; set; }
    }
}
