using System;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using UserStore.BLL.DTO;
using UserStore.BLL.Services;
using UserStore.BLL.Interfaces;
using UserStore.BLL.Infrastructure;
using Web.Models;
using Ninject;

namespace Web.Controllers
{
    public class AdminController : Controller
    {

        private IUserService userService;

        public AdminController(IUserService us)
        {
            userService = us;
        }
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            List<UserViewModel> allUsers = Mapper.Map<List<UserViewModel>>(userService.GetAllUsers());
            for (int i = 0; i < allUsers.Count; i++)
            {
                allUsers[i].Roles = userService.GetRoles(allUsers[i].Id);
            }
            return View(allUsers);
        }

        [HttpGet]
        public ActionResult Edit(UserViewModel userViewModel)
        {
            UserViewModel user = Mapper.Map<UserViewModel>(userViewModel);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel userViewModel, string role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userService.Update(userViewModel.UserName, role);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(userViewModel);
        }

        public ActionResult Delete(string id)
        {
            //try
            //{
                UserViewModel user = Mapper.Map<UserViewModel>(userService.FindByName(id));
                return View(user);
            /*}
            catch (DataException e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View();*/
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
                userService.DeleteUser(id);
                return RedirectToAction("Index");
        }
	}
}