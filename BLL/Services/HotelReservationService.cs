using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repository;
using DAL.Interfaces;
using BLL.DTO;
using BLL.Interfaces;
using AutoMapper;

namespace BLL.Services
{
    public class HotelReservationService : IHotelReservationService
    {
        private IUnitOfWork uow;

        public HotelReservationService(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }
        public void Reserve(HotelReservationDTO reservation)
        {
            HotelReservation newReservation = new HotelReservation()
            {
                ClientName = reservation.ClientName,
                Hotel = uow.Hotels.FindById(reservation.Hotel.Id),
                NumberOfPersons = reservation.NumberOfPersons,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate
            };

            uow.HotelReservations.Create(newReservation);
            uow.Save();
        }

        public void DeleteReservation(int reservationId)
        {
            HotelReservation reservation = uow.HotelReservations.FindById(reservationId);

            if (reservation == null)
                throw new Exception("Order with id " + reservationId + " was not found");

            uow.HotelReservations.Remove(reservation);
            uow.Save();
        }

        public List<HotelReservationDTO> GetAllReservationsByClient(string clientName)
        {
            List<HotelReservation> allReservationsByUser = new List<HotelReservation>();
            foreach (HotelReservation i in uow.HotelReservations.Get())
            {
                if (i.ClientName == clientName)
                {
                    allReservationsByUser.Add(i);
                }
            }

            return Mapper.Map<List<HotelReservationDTO>>(allReservationsByUser);
        }

        public HotelReservationDTO FindById(int id)
        {
            HotelReservation reservation = uow.HotelReservations.FindById(id);

            if (reservation == null)
                throw new Exception("Reservation with id " + id + " was not found");

            return Mapper.Map<HotelReservationDTO>(reservation);
        }
    }
}
