using ECommerceRestApi.Core.Service;
using ECommerceRestApi.Models;
namespace ECommerceRestApi.Services.Abstract
{
    public interface IOrderService : IBaseService<Order,Guid>
    {
        List<Order> GetAllByUserWithDetails(Guid userId);
    }
}
