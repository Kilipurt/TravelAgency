using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using Web.Models;
using System.Security.Claims;
using BLL.Services;
using AutoMapper;
using BLL.Interfaces;
using BLL.DTO;
using Ninject;

namespace Web.Controllers
{
    public class TourController : Controller
    {
        private IOutputTourService outputTourService;
        private ITourService tourService;

        public TourController(IOutputTourService ots, ITourService ts)
        {
            outputTourService = ots;
            tourService = ts;
        }

        public ActionResult Index(FilterTourViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tours = outputTourService.GetAllFilteredTours(model.Country, model.Region, model.Duration, model.isHotTours);

                return View(Mapper.Map<List<TourViewModel>>(tours));
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "manager")]
        public ActionResult Create()
        {
            return View(new TourViewModel { Country = "Country", Region = "Region", StartDate = new DateTime(2018, 1, 1), EndDate = new DateTime(2018, 1, 1), Hotel = "Hotel"});
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public ActionResult Create(TourViewModel tourViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TourDTO newTour = new TourDTO()
                    {
                        Country = tourViewModel.Country,
                        Region = tourViewModel.Region,
                        StartDate = tourViewModel.StartDate,
                        EndDate = tourViewModel.EndDate,
                        Hotel = tourViewModel.Hotel
                    };
                    tourService.CreateTour(newTour);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */ )
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(tourViewModel);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                TourViewModel tour = Mapper.Map<TourViewModel>(tourService.FindById(id));
                return View(tour);
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
                tourService.DeleteTour(id);
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