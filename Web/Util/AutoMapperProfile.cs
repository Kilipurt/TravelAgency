using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using UserStore.BLL.DTO;
using Web.Models;
using BLL.DTO;

namespace Web.Util
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDTO, UserViewModel>().ForMember(dest => dest.Roles, opt => opt.Ignore());
            CreateMap<OrderDTO, OrderViewModel>().ForMember("TourId", opt => opt.MapFrom(src => src.Tour.TourId));
            CreateMap<TourDTO, TourViewModel>();
            CreateMap<HotelReservationDTO, HotelReservationViewModel>().ForMember("HotelId", opt => opt.MapFrom(src => src.Hotel.Id));
            CreateMap<HotelDTO, HotelViewModel>();
        }
    }
}