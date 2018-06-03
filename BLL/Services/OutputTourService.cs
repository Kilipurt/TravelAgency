using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repository;
using Ninject;
using AutoMapper;
using BLL.Interfaces;

namespace BLL.Services
{
    public class OutputTourService : IOutputTourService
    {
        private IUnitOfWork uow;

        public OutputTourService(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }

        public List<TourDTO> GetAllFilteredTours(string country, string region, int? duration, Boolean isHotTours)
        {
            List<TourDTO> tours = new List<TourDTO>();
            if (isHotTours == true)
            {
                tours = GetHotTours();
            }
            else
            {
                tours = Mapper.Map<List<TourDTO>>(uow.Tours.Get());
            }

            return ToursFilterByDuration(duration, ToursFilterByRegion(region, ToursFilterByCountry(country, tours)));
        }

        public List<TourDTO> GetHotTours()
        {
            List<Tour> hotTours = new List<Tour>();

            foreach (Tour tour in uow.Tours.Get())
            {
                if (tour.StartDate.Subtract(DateTime.Now).Days <= 10)
                {
                    hotTours.Add(tour);
                }
            }

            return Mapper.Map<List<TourDTO>>(hotTours);
        }

        private List<TourDTO> ToursFilterByCountry(string country, List<TourDTO> tours)
        {
            if (country == null)
            {
                return tours;
            }

            List<TourDTO> filteredToursByCountry = new List<TourDTO>();

            foreach (TourDTO tour in tours)
            {
                if (tour.Country == country)
                {
                    filteredToursByCountry.Add(tour);
                }
            }

            return filteredToursByCountry;
        }

        private List<TourDTO> ToursFilterByRegion(string region, List<TourDTO> tours)
        {
            List<TourDTO> filteredToursByRegion = new List<TourDTO>();

            if (region == null)
            {
                return tours;
            }

            foreach (TourDTO tour in tours)
            {
                if (tour.Region == region)
                {
                    filteredToursByRegion.Add(tour);
                }
            }

            return filteredToursByRegion;
        }

        private List<TourDTO> ToursFilterByDuration(int? duration, List<TourDTO> tours)
        {
            if (duration <= 0 || duration == null)
            {
                return tours;
            }

            List<TourDTO> filteredToursByDuration = new List<TourDTO>();

            foreach (TourDTO tour in tours)
            {
                if (tour.EndDate.Subtract(tour.StartDate).Days == duration)
                {
                    filteredToursByDuration.Add(tour);
                }
            }

            return filteredToursByDuration;
        }
    }
}