using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IOrderService
    {
        void CreateOrder(OrderDTO order);
        OrderDTO FindById(int id);
        void DeleteOrder(int orderId);
        List<OrderDTO> GetAllOrders();
        List<OrderDTO> GetAllOrdersByClient(string clientName);
    }
}
