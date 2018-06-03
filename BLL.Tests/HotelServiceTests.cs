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
    public class HotelServiceTests
    {
        [TestMethod]
        public void CreateHotelTest()
        {
            ResetData();
            var uow = new Mock<UnitOfWork>();
            HotelService hs = new HotelService(uow.Object);
            int numEx = hs.GetAllHotels().Count + 1;

            HotelDTO hotel = new HotelDTO() { Name = "Test", Address = "Address" };
            hs.CreateHotel(hotel);
            int numAc = hs.GetAllHotels().Count;

            Assert.AreEqual(numEx, numAc);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CreateHotelTestWithException()
        {
            ResetData();
            var uow = new Mock<UnitOfWork>();
            HotelService hs = new HotelService(uow.Object);

            hs.CreateHotel(null);
        }

        [TestMethod]
        public void DeleteHotelTest()
        {
            ResetData();
            var uow = new Mock<UnitOfWork>();
            HotelService hs = new HotelService(uow.Object);
            HotelDTO hotel = new HotelDTO() { Name = "Test", Address = "Address" };
            hs.CreateHotel(hotel);
            int numEx = hs.GetAllHotels().Count - 1;

            hs.DeleteHotel(hs.GetAllHotels()[hs.GetAllHotels().Count - 1].Id);

            Assert.AreEqual(numEx, hs.GetAllHotels().Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DeleteHotelTestWithException()
        {
            ResetData();
            var uow = new Mock<UnitOfWork>();
            HotelService hs = new HotelService(uow.Object);

            hs.DeleteHotel(-5);
        }

        [TestMethod]
        public void FindByIdTest()
        {
            ResetData();
            var uow = new Mock<UnitOfWork>();
            HotelService hs = new HotelService(uow.Object);
            HotelDTO hotel = new HotelDTO() { Name = "Test", Address = "Address" };
            hs.CreateHotel(hotel);

            HotelDTO searchedHotel = hs.FindById(hs.GetAllHotels()[hs.GetAllHotels().Count - 1].Id);

            Assert.AreEqual(hotel.Name, searchedHotel.Name);
            Assert.AreEqual(hotel.Address, searchedHotel.Address);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void FindByIdTestWithException()
        {
            ResetData();
            var uow = new Mock<UnitOfWork>();
            HotelService hs = new HotelService(uow.Object);

            hs.FindById(-5);
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