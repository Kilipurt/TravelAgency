using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repository;
using AutoMapper;
using BLL.DTO;
using Ninject;
using BLL.Interfaces;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork uow;

        public OrderService(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }

        public void CreateOrder(OrderDTO order)
        {
            Tour tour = uow.Tours.FindById(order.Tour.TourId);

            if (tour == null)
                throw new Exception("Tour was not found");

            Order newOrder = new Order()
            {
                Tour = uow.Tours.FindById(order.TourId),
                ClientName = order.ClientName,
                NumberOfPerson = order.NumberOfPerson,
                Transport = order.Transport,
                TourId = order.TourId,
                Hotel = order.Hotel
            };

            uow.Orders.Create(newOrder);
            uow.Save();

            tour.Orders.Add(newOrder);
            uow.Save();
        }

        public OrderDTO FindById(int id)
        {
            Order order = uow.Orders.FindById(id);

            if (order == null)
                throw new Exception("Order with id " + id + " was not found");

            return Mapper.Map<OrderDTO>(order);
        }

        public void DeleteOrder(int orderId)
        {
            Order order = uow.Orders.FindById(orderId);

            if (order == null)
                throw new Exception("Order with id " + orderId + " was not found");

            uow.Orders.Remove(order);
            uow.Save();
        }

        public List<OrderDTO> GetAllOrders()
        {
            return Mapper.Map<List<OrderDTO>>(uow.Orders.Get());
        }

        public List<OrderDTO> GetAllOrdersByClient(string clientName)
        {
            List<Order> allOdrersByUser = new List<Order>();
            foreach (Order i in uow.Orders.Get())
            {
                if (i.ClientName == clientName)
                {
                    allOdrersByUser.Add(i);
                }
            }

            return Mapper.Map<List<OrderDTO>>(allOdrersByUser);
        }
    }
}