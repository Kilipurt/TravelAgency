using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IHotelService
    {
        void CreateHotel(HotelDTO hotel);
        HotelDTO FindById(int id);
        void DeleteHotel(int id);
        List<HotelDTO> GetAllHotels();
    }
}
