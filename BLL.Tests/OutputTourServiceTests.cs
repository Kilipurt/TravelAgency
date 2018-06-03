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
    public class OutputTourServiceTests
    {
        [TestMethod]
        public void GetAllFilteredToursTest()
        {
            ResetData();
            Mock<DAL.Interfaces.IUnitOfWork> mockUOW = new Mock<DAL.Interfaces.IUnitOfWork>();
            mockUOW.Setup(repo => repo.Tours.Get()).Returns(GetTestTours());

            Assert.AreEqual(GetTestTours().Count, 4);
        }

        private List<Tour> GetTestTours()
        {
            var tours = new List<Tour>
            {
                new Tour { TourId=1, Country="qwer", Hotel="ewf", Region="qwef", StartDate = DateTime.Now, EndDate = DateTime.Now},
                new Tour { TourId=2, Country="qwefc", Hotel="qewf", Region="wqf", StartDate = DateTime.Now, EndDate = DateTime.Now},
                new Tour { TourId=3, Country="rewq", Hotel="wqf", Region="qwf", StartDate = DateTime.Now, EndDate = DateTime.Now},
                new Tour { TourId=4, Country="sdg", Hotel="qwf", Region="ewg", StartDate = DateTime.Now, EndDate = DateTime.Now},
            };
            return tours;
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
