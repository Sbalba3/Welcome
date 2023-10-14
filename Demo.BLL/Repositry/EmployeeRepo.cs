using Demo.BLL.Interfaces;
using Demo.DAl.Context;
using Demo.DAl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositry
{
    public class EmployeeRepo:GenricRepo<Employee>,IEmployeeRepo
    {
       
        public EmployeeRepo(WelcomeContext welcomeContext):base(welcomeContext) 
        {
           
            
        }
        public IQueryable<Employee> GetByAddress(string address)
        {
            return _welcomeContext.Employees.Where(x => x.Address == address);
        }


        //public IQueryable<Employee> SearchByName(string name)
        //{
        //    return _welcomeContext.Employees.Where(x => x.Name.ToLower().Contains(name.ToLower()));
        //}
    }
}
