using BLL.Services.Abstraction;
using DAL.Entities;
using DAL.Repository.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services.Implementation
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IGenericRepository<Developer> repoDev;
        public DeveloperService(IGenericRepository<Developer> _repoDev)
        {
            repoDev = _repoDev;
        }
        public void AddDeveloper(Developer developer)
        {
            repoDev.Create(developer);
        }

        public void DeleteDeveloper(Developer developer)
        {
            repoDev.Delete(developer);
        }

        public Developer Find(int id)
        {
            return repoDev.Find(id);
        }

        public ICollection<Developer> GetAllDevelopers()
        {
            return repoDev.GetAll().ToList();
        }
    }
}
