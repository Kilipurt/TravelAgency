using System;
using System.Collections.Generic;
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLL.Tests
{
    [TestClass]
    public class HotelReservationServiceTests
    {
        [TestMethod]
        public void ReserveTest()
        {
            ResetData();
            var uow = new Mock<UnitOfWork>();
            HotelReservationService hrs = new HotelReservationService(uow.Object);
            int numEx = uow.Object.HotelReservations.Get().Count + 1;

            HotelReservationDTO reservation = new HotelReservationDTO()
            {
                NumberOfPersons = 2,
                ClientName = "ewf",
                Hotel = Mapper.Map<HotelDTO>(uow.Object.Hotels.Get()[0]),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };
            hrs.Reserve(reservation);

            Assert.AreEqual(numEx, uow.Object.HotelReservations.Get().Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ReserveTestWithException()
        {
            ResetData();
            var uow = new Mock<UnitOfWork>();
            HotelReservationService hrs = new HotelReservationService(uow.Object);

            hrs.Reserve(null);
        }

        [TestMethod]
        public void DeleteReservationTest()
        {
            ResetData();
            var uow = new Mock<UnitOfWork>();
            HotelReservationService hrs = new HotelReservationService(uow.Object);
            int numEx = uow.Object.HotelReservations.Get().Count - 1;

            HotelReservation reservation = uow.Object.HotelReservations.Get()[uow.Object.HotelReservations.Get().Count - 1];
            hrs.DeleteReservation(reservation.HotelReservationId);

            Assert.AreEqual(numEx, uow.Object.HotelReservations.Get().Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DeleteReservationTestWithException()
        {
            ResetData();
            var uow = new Mock<UnitOfWork>();
            HotelReservationService hrs = new HotelReservationService(uow.Object);

            hrs.DeleteReservation(-5);
        }

        [TestMethod]
        public void FindByIdTest()
        {
            ResetData();
            var uow = new Mock<UnitOfWork>();
            HotelReservationService hrs = new HotelReservationService(uow.Object);

            HotelReservation reservation = uow.Object.HotelReservations.Get()[uow.Object.HotelReservations.Get().Count - 1];
            HotelReservationDTO res = hrs.FindById(reservation.HotelReservationId);

            Assert.AreEqual(reservation.ClientName, res.ClientName);
            Assert.AreEqual(reservation.Hotel.Name, res.Hotel.Name);
            Assert.AreEqual(reservation.StartDate, res.StartDate);
            Assert.AreEqual(reservation.EndDate, res.EndDate);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void FindByIdWithException()
        {
            ResetData();
            var uow = new Mock<UnitOfWork>();
            HotelReservationService hrs = new HotelReservationService(uow.Object);

            hrs.FindById(-5);
        }

        private void ResetData()
        {
            Mapper.Initialize(cfg => cfg.AddProfiles
                (new[] { 
                    typeof(UserStore.BLL.Util.AutoMapperProfile), 
                    typeof(BLL.Util.AutoMapperProfile) 
                }));
        }
    }
}
