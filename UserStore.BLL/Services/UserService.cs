using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStore.BLL.DTO;
using UserStore.BLL.Infrastructure;
using UserStore.DAL.Entities;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using UserStore.BLL.Interfaces;
using UserStore.DAL.Interfaces;
using UserStore.DAL.Repositories;
using AutoMapper;
using Ninject;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UserStore.BLL.Services
{
    public class UserService : IUserService
    {
        public IUnitOfWork Database;

        [Inject]
        public UserService()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IUnitOfWork>().To<IdentityUnitOfWork>();

            Database = ninjectKernel.Get<IUnitOfWork>();
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                // создаем профиль клиента
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public List<UserDTO> GetAllUsers()
        {
            return Mapper.Map<List<UserDTO>>(Database.ClientManager.Get());
        }

        public UserDTO FindByName(string id)
        {
            return Mapper.Map<UserDTO>(Database.UserManager.FindByName(id));
        }

        public IEnumerable<string> GetRoles(string id)
        {
            return Database.UserManager.GetRoles(id);
        }

        public void Update(string id, string role)
        {
            ApplicationUser user = Database.UserManager.FindByName(id);

            if (user == null)
                throw new DataException("User " + id + " was not found");

            string[] roles = new string[user.Roles.Count];

            int count = 0;
            foreach (IdentityUserRole r in user.Roles)
            {
                roles[count] = r.ToString();
                count++;
            }

            Database.UserManager.RemoveFromRoles(user.Id, roles);
            Database.UserManager.AddToRole(user.Id, role);
            Database.SaveAsync();
        }

        public void DeleteUser(string id)
        {
            ApplicationUser user = Database.UserManager.FindByName(id);

            if (user == null)
                throw new DataException("User " + id + " was not found");

            Database.ClientManager.Delete(user.ClientProfile.Id);
            Database.UserManager.Delete(user);
            Database.SaveAsync();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
