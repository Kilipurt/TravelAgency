using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repository;
using BLL.DTO;
using BLL.Services;
using UserStore.BLL.Services;
using UserStore.DAL.Repositories;
using UserStore.BLL.DTO;
using UserStore.DAL.Interfaces;
using Moq;
using AutoMapper;

namespace BLL.Tests
{
    [TestClass]
    public class OrderServiceTests
    {
        private void ResetData()
        {
            Mapper.Initialize(cfg => cfg.AddProfiles
                (new[] { 
                    typeof(UserStore.BLL.Util.AutoMapperProfile), 
                    typeof(BLL.Util.AutoMapperProfile) 
                }));
        }

        [TestMethod]
        public void CreateOrderTest()
        {
            ResetData();
            var uow = new Mock<UnitOfWork>();
            var identityUow = new Mock<IdentityUnitOfWork>();

            UserService us = new UserService();
            TourService ts = new TourService(uow.Object);
            OrderService os = new OrderService(uow.Object);
            OutputTourService ots = new OutputTourService(uow.Object);

            UserDTO user = new UserDTO() { Email = "123", Password = "123456", Address = "Kiev", Name = "Andrey" };
            us.Create(user);
            us.Authenticate(user);

            TourDTO tour = new TourDTO() { Country = "Ukraine", Region = "Kiev", Hotel = "Ukraine", EndDate = DateTime.Now, StartDate = DateTime.Now };
            ts.CreateTour(tour);

            List<TourDTO> tours = ots.GetAllFilteredTours(null, null, null, false);
            OrderDTO order = new OrderDTO() { Tour = tours[tours.Count - 1], TourId = tours[tours.Count - 1].TourId, ClientName = user.Email, Hotel = true, NumberOfPerson = 1, Transport = true };
            os.CreateOrder(order);

            Assert.AreEqual(os.GetAllOrders()[os.GetAllOrders().Count - 1].Tour.Country, tour.Country);
        }

        [TestMethod]
        public void GetAllOrdersByClient()
        {
            ResetData();
            var uow = new Mock<UnitOfWork>();
            var identityUow = new Mock<IdentityUnitOfWork>();

            UserService us = new UserService();
            OrderService os = new OrderService(uow.Object);

            for (int i = 0; i < os.GetAllOrders().Count; i++)
            {
                os.DeleteOrder(os.GetAllOrders()[i].Id);
            }

            int num = 0;
            for (int i = us.GetAllUsers().Count - 1; i >= 0; i--)
            {
                num += os.GetAllOrdersByClient(us.GetAllUsers()[i].Email).Count;
            }

            Assert.AreEqual(os.GetAllOrders().Count, num);
        }

        [TestMethod]
        public void GetAllOrdersTest()
        {
            ResetData();
            Mock<DAL.Interfaces.IUnitOfWork> mockUOW = new Mock<DAL.Interfaces.IUnitOfWork>();
            mockUOW.Setup(repo => repo.Orders.Get()).Returns(GetTestOrders());

            Assert.AreEqual(GetTestOrders().Count, 4);
        }

        [TestMethod]
        public void FindByIdTest()
        {
            ResetData();
            var uow = new Mock<UnitOfWork>();
            TourService ts = new TourService(uow.Object);
            OutputTourService ots = new OutputTourService(uow.Object);

            TourDTO tour = new TourDTO() { Country = "Ukraine", Region = "Kiev", Hotel = "Ukraine", EndDate = DateTime.Now, StartDate = DateTime.Now };
            ts.CreateTour(tour);

            List<TourDTO> tours = ots.GetAllFilteredTours(null, null, null, false);
            Assert.AreEqual(ts.FindById(tours[tours.Count - 1].TourId).Country, tour.Country);
        }

        [TestMethod]
        public void DeleteOrderTest()
        {
            ResetData();
            var uow = new Mock<UnitOfWork>();
            OrderService os = new OrderService(uow.Object);

            int num = os.GetAllOrders().Count;
            os.DeleteOrder(os.GetAllOrders()[os.GetAllOrders().Count - 1].Id);

            Assert.AreEqual(os.GetAllOrders().Count, num - 1);
        }

        private List<Order> GetTestOrders()
        {
            var orders = new List<Order>
            {
                new Order { Id=1, ClientName="qwer", Hotel=true, NumberOfPerson=9, Transport = false, TourId = 1},
                new Order { Id=2, ClientName="qwefc", Hotel=true, NumberOfPerson=3, Transport = false, TourId = 2},
                new Order { Id=3, ClientName="rewq", Hotel=false, NumberOfPerson=4, Transport = false, TourId = 3},
                new Order { Id=4, ClientName="sdg", Hotel=true, NumberOfPerson=9, Transport = false, TourId = 4},
            };
            return orders;
        }
    }
}