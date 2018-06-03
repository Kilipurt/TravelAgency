using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStore.DAL.EF;
using UserStore.DAL.Entities;
using UserStore.DAL.Interfaces;

namespace UserStore.DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public ApplicationContext Database { get; set; }
        public ClientManager(ApplicationContext db)
        {
            Database = db;
        }

        public void Create(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

        public void Delete(string id)
        {
            Database.ClientProfiles.Remove(Database.ClientProfiles.Find(id));
        }

        public IEnumerable<ApplicationUser> Get()
        {
            return Database.Users;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
