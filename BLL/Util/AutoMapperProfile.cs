using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Entities;
using BLL.DTO;

namespace BLL.Util
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Order, OrderDTO>();

            CreateMap<Tour, TourDTO>();

            CreateMap<Hotel, HotelDTO>();

            CreateMap<HotelReservation, HotelReservationDTO>();
        }
    }
}
