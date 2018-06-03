using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using UserStore.DAL.Entities;
using UserStore.BLL.DTO;

namespace UserStore.BLL.Util
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, UserDTO>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ClientProfile.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.ClientProfile.Address))
                .ForMember(dest => dest.Role, opt => opt.Ignore());
        }
    }
}
