using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Ninject;
using AutoMapper;
using DAL.Repository;
using BLL.DTO;
using DAL.Entities;
using BLL.Interfaces;

namespace BLL.Services
{
    public class TourService : ITourService
    {
        private IUnitOfWork uow;

        public TourService(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }

        public void CreateTour(TourDTO tour)
        {
            Tour newTour = new Tour()
            {
                Country = tour.Country,
                Region = tour.Region,
                StartDate = tour.StartDate,
                EndDate = tour.EndDate,
                Hotel = tour.Hotel
            };

            uow.Tours.Create(newTour);
            uow.Save();
        }

        public TourDTO FindById(int id)
        {
            Tour tour = uow.Tours.FindById(id);

            if (tour == null)
                throw new Exception("Tour with id " + id + " was not found");

            return Mapper.Map<TourDTO>(tour);
        }

        public void DeleteTour(int id)
        {
            Tour tour = uow.Tours.FindById(id);

            if (tour == null)
                throw new Exception("Tour with id " + id + " was not found");

            uow.Tours.Remove(tour);
            uow.Save();
        }
    }
}
