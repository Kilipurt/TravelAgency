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
    public class HotelReservationController : Controller
    {
        private IHotelReservationService hotelReservationService;
        private IHotelService hotelService;

        public HotelReservationController(IHotelReservationService hrs, IHotelService hs)
        {
            hotelReservationService = hrs;
            hotelService = hs;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View(Mapper.Map<List<HotelReservationViewModel>>(hotelReservationService.GetAllReservationsByClient(User.Identity.Name)));
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create(int id)
        {
            return View(new HotelReservationViewModel { HotelId = id, StartDate = DateTime.Now, EndDate = DateTime.Now, NumberOfPersons = 1 });
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(HotelReservationViewModel hotelReservationViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HotelReservationDTO newReservation = new HotelReservationDTO()
                    {
                        ClientName = User.Identity.Name,
                        StartDate = hotelReservationViewModel.StartDate,
                        EndDate = hotelReservationViewModel.EndDate,
                        Hotel = hotelService.FindById(hotelReservationViewModel.HotelId),
                        NumberOfPersons = hotelReservationViewModel.NumberOfPersons
                    };

                    hotelReservationService.Reserve(newReservation);
                    return RedirectToAction("Index", "Hotel");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            return View(hotelReservationViewModel);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                HotelReservationViewModel reservation = Mapper.Map<HotelReservationViewModel>(hotelReservationService.FindById(id));
                return View(reservation);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                hotelReservationService.DeleteReservation(id);
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