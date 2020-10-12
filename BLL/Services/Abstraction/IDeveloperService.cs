using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstraction
{
    public interface IDeveloperService
    {
        void AddDeveloper(Developer developer);
        void DeleteDeveloper(Developer developer);
        ICollection<Developer> GetAllDevelopers();
        Developer Find(int id);
    }
}
