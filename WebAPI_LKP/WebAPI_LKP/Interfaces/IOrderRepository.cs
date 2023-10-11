using WebAPI_LKP.Models;

namespace WebAPI_LKP.Interfaces
{
    public interface IOrderRepository
    {
        Order? GetOrderByUser(User user);
    }
}
