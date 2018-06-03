using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IHotelReservationService
    {
        void Reserve(HotelReservationDTO reservation);
        void DeleteReservation(int reservationId);
        List<HotelReservationDTO> GetAllReservationsByClient(string clientName);
        HotelReservationDTO FindById(int id);
    }
}
