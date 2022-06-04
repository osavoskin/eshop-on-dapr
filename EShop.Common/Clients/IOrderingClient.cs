using EShop.Common.Models;
using System.Threading.Tasks;

namespace EShop.Common.Clients
{
    public interface IOrderingClient
    {
        Task PlaceOrder(Order order);
    }
}
