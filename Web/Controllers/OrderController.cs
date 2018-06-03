using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Services;
using AutoMapper;
using BLL.Interfaces;
using BLL.DTO;
using System.Security.Claims;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using Web.Models;
using Ninject;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        private IOrderService orderService;
        private ITourService tourService;

        public OrderController(IOrderService os, ITourService ts)
        {
            orderService = os;
            tourService = ts;
        }
        //
        // GET: /Order/
        [Authorize]
        public ActionResult Index()
        {
            return View(Mapper.Map<List<OrderViewModel>>(orderService.GetAllOrdersByClient(User.Identity.Name)));
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create(int id)
        {
            return View(new OrderViewModel {TourId = id, NumberOfPerson = 1, Hotel = true, Transport = true });
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    OrderDTO newOrder = new OrderDTO()
                    {
                        ClientName = User.Identity.Name,
                        Tour = tourService.FindById(orderViewModel.TourId),
                        Transport = orderViewModel.Transport,
                        Hotel = orderViewModel.Hotel,
                        TourId = orderViewModel.TourId,
                        NumberOfPerson = orderViewModel.NumberOfPerson
                    };

                    orderService.CreateOrder(newOrder);
                    return RedirectToAction("Index", "Tour");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            return View(orderViewModel);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                OrderViewModel order = Mapper.Map<OrderViewModel>(orderService.FindById(id));
                return View(order);
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
                orderService.DeleteOrder(id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View();
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            try
            {
                return View(Mapper.Map<OrderViewModel>(orderService.FindById(id)));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View();
        }
	}
}