using Kitchen.Data.DataContexts;
using Kitchen.Data.Models;
using System;
using System.Threading.Tasks;

namespace Kitchen.Data.DAL
{

    public class UnitOfWork : IDisposable
    {
        public KitchenContext _Context;
        private DataRepository<Client> clientRepository;
        private DataRepository<User> userRepository;
        private DataRepository<Resident> residentRepository;
        private DataRepository<MenuCatalog> menuCatalogRepository;
        private DataRepository<DailyMenu> dailyMenuRepository;
        private DataRepository<MenuItem> menuItemshipRepository;
        private DataRepository<Group> groupRepository;
        private DataRepository<MealRequest> mealRequestRepository;

        public UnitOfWork(KitchenContext Context)
        {
            _Context = Context;
        }

        public DataRepository<MealRequest> MealRequestRepository
        {
            get
            {

                if (this.mealRequestRepository == null)
                {
                    this.mealRequestRepository = new DataRepository<MealRequest>(_Context);
                }
                return mealRequestRepository;
            }
        }



        public DataRepository<Group> GroupRepository
        {
            get
            {

                if (this.groupRepository == null)
                {
                    this.groupRepository = new DataRepository<Group>(_Context);
                }
                return groupRepository;
            }
        }

        public DataRepository<Client> ClientRepository
        {
            get
            {

                if (this.clientRepository == null)
                {
                    this.clientRepository = new DataRepository<Client>(_Context);
                }
                return clientRepository;
            }
        }

        public DataRepository<User> UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new DataRepository<User>(_Context);
                }
                return userRepository;
            }
        }


        public DataRepository<Resident> ResidentRepository
        {
            get
            {

                if (this.residentRepository == null)
                {
                    this.residentRepository = new DataRepository<Resident>(_Context);
                }
                return residentRepository;
            }
        }


        public DataRepository<MenuCatalog> MenuCatalogRepository
        {
            get
            {

                if (this.menuCatalogRepository == null)
                {
                    this.menuCatalogRepository = new DataRepository<MenuCatalog>(_Context);
                }
                return menuCatalogRepository;
            }
        }

        public DataRepository<DailyMenu> DailyMenuRepository
        {
            get
            {

                if (this.dailyMenuRepository == null)
                {
                    this.dailyMenuRepository = new DataRepository<DailyMenu>(_Context);
                }
                return dailyMenuRepository;
            }
        }


        public DataRepository<MenuItem> MenuItemRepository
        {
            get
            {

                if (this.menuItemshipRepository == null)
                {
                    this.menuItemshipRepository = new DataRepository<MenuItem>(_Context);
                }
                return menuItemshipRepository;
            }
        }




        public async Task<int> CommitAsync()
        {
            return await _Context.SaveChanges();
        }

        public void Dispose()
        {
            _Context.Dispose();
        }
    }

}
