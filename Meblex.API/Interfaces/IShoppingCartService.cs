using System.Collections.Generic;
using Meblex.API.FormsDto.Request;
using Meblex.API.FormsDto.Response;

namespace Meblex.API.Interfaces
{
    public interface IShoppingCartService
    {
        List<OrderResponse> GetAllClientOrders(int userId);
        OrderResponse GetClientById(int id, int userId);
        int AddOrder(int userId, OrderAddForm order);
    }
}