using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Services;
using AutoMapper;
using BLL.Interfaces;
using BLL.DTO;
using Ninject;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Mvc;
using Web.Models;
using System.Data;

namespace Web.Controllers
{
    public class HotelController : Controller
    {
        private IHotelService hotelService;

        public HotelController(IHotelService hs)
        {
            hotelService = hs;
        }

        public ActionResult Index()
        {
            var hotels = hotelService.GetAllHotels();

            return View(Mapper.Map<List<HotelViewModel>>(hotels));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new HotelViewModel { Name = "HotelName", Address = "HotelAdress" });
        }

        [HttpPost]
        public ActionResult Create(HotelViewModel hotelViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HotelDTO newHotel = new HotelDTO()
                    {
                        Name = hotelViewModel.Name,
                        Address = hotelViewModel.Address
                    };
                    hotelService.CreateHotel(newHotel);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */ )
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(hotelViewModel);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                HotelViewModel hotel = Mapper.Map<HotelViewModel>(hotelService.FindById(id));
                return View(hotel);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                hotelService.DeleteHotel(id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View();
        }
    }
}