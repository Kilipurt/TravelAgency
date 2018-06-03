using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using UserStore.BLL.Interfaces;
using UserStore.BLL.Services;
using BLL.Interfaces;
using BLL.Services;

namespace Web.Util
{
    public class UserModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserService>();
        }
    }
}