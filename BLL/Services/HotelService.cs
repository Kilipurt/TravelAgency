using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repository;
using AutoMapper;

namespace BLL.Services
{
    public class HotelService : IHotelService
    {
        private IUnitOfWork uow;

        public HotelService(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }

        public void CreateHotel(HotelDTO hotel)
        {
            Hotel newHotel = new Hotel()
            {
                Name = hotel.Name,
                Address = hotel.Address
            };

            uow.Hotels.Create(newHotel);
            uow.Save();
        }

        public HotelDTO FindById(int id)
        {
            Hotel hotel = uow.Hotels.FindById(id);

            if (hotel == null)
                throw new Exception("Hotel with id " + id + " was not found");

            return Mapper.Map<HotelDTO>(hotel);
        }

        public void DeleteHotel(int id)
        {
            Hotel hotel = uow.Hotels.FindById(id);

            if (hotel == null)
                throw new Exception("Hotel with id " + id + " was not found");

            uow.Hotels.Remove(hotel);
            uow.Save();
        }

        public List<HotelDTO> GetAllHotels()
        {
            return Mapper.Map<List<HotelDTO>>(uow.Hotels.Get());
        }
    }
}
