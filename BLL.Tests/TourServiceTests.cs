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
    public class TourServiceTests
    {
        [TestMethod]
        public void CreateTourTest()
        {
            ResetData();
            var uow = new Mock<UnitOfWork>();
            TourService ts = new TourService(uow.Object);
            OutputTourService ots = new OutputTourService(uow.Object);
            int numEx = ots.GetAllFilteredTours(null, null, null, false).Count + 1;

            TourDTO tour = new TourDTO() { Country = "Test", Hotel = "Test", Region = "Test", EndDate = DateTime.Now, StartDate = DateTime.Now };
            ts.CreateTour(tour);
            int numAc = ots.GetAllFilteredTours(null, null, null, false).Count;

            Assert.AreEqual(numEx, numAc);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CreateTourTestWithException()
        {
            ResetData();
            var uow = new Mock<UnitOfWork>();
            TourService ts = new TourService(uow.Object);
            ts.CreateTour(null);
        }

        [TestMethod]
        public void FindByIdTest()
        {
            ResetData();
            var uow = new Mock<UnitOfWork>();
            TourService ts = new TourService(uow.Object);
            OutputTourService ots = new OutputTourService(uow.Object);

            TourDTO tour = ots.GetAllFilteredTours(null, null, null, false)[0];

            Assert.AreEqual(tour.Country, ts.FindById(tour.TourId).Country);
            Assert.AreEqual(tour.Region, ts.FindById(tour.TourId).Region);
            Assert.AreEqual(tour.Hotel, ts.FindById(tour.TourId).Hotel);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void FindByIdTestWithException()
        {
            ResetData();
            var uow = new Mock<UnitOfWork>();
            TourService ts = new TourService(uow.Object);
            ts.FindById(-5);
        }

        [TestMethod]
        public void DeleteTourTest()
        {
            ResetData();
            var uow = new Mock<UnitOfWork>();
            TourService ts = new TourService(uow.Object);
            OutputTourService ots = new OutputTourService(uow.Object);

            TourDTO tour = new TourDTO() { Country = "Test", Hotel = "Test", Region = "Test", EndDate = DateTime.Now, StartDate = DateTime.Now };
            ts.CreateTour(tour);
            List<TourDTO> tours = ots.GetAllFilteredTours(null, null, null, false);
            int numEx = tours.Count - 1;
            ts.DeleteTour(tours[tours.Count - 1].TourId);
            int numAc = ots.GetAllFilteredTours(null, null, null, false).Count;

            Assert.AreEqual(numEx, numAc);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DeleteTourTestWithException()
        {
            ResetData();
            var uow = new Mock<UnitOfWork>();
            TourService ts = new TourService(uow.Object);

            ts.DeleteTour(-5);
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
