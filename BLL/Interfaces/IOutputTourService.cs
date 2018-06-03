using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IOutputTourService
    {
        List<TourDTO> GetAllFilteredTours(string country, string region, int? duration, Boolean isHotTours);
        List<TourDTO> GetHotTours();
    }
}
