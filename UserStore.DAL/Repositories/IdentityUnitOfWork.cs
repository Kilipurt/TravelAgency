using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStore.DAL.EF;
using UserStore.DAL.Entities;
using UserStore.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using UserStore.DAL.Identity;
using Ninject;

namespace UserStore.DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private ApplicationContext db = new ApplicationContext("DefaultConnection");

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IClientManager clientManager;

        /*public IdentityUnitOfWork(string connectionString)
        {
            db = new ApplicationContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            clientManager = new ClientManager(db);
        }*/

        [Inject]
        public IdentityUnitOfWork()
        {

        }

        public ApplicationUserManager UserManager
        {
            get 
            { 
                if (this.userManager == null)
                {
                    this.userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
                }

                return userManager; 
            }
        }

        public IClientManager ClientManager
        {
            get 
            { 
                if (this.clientManager == null)
                {
                    this.clientManager = new ClientManager(db);
                }

                return clientManager; 
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get 
            {
                if (this.roleManager == null)
                {
                    this.roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
                }

                return roleManager; 
            }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
