using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using BLL.Interfaces;
using BLL.Services;

namespace Web.Util
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IOrderService>().To<OrderService>();
            Bind<IOutputTourService>().To<OutputTourService>();
            Bind<ITourService>().To<TourService>();
            Bind<IHotelService>().To<HotelService>();
            Bind<IHotelReservationService>().To<HotelReservationService>();
        }
    }
}