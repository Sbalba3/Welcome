using Demo.BLL.Interfaces;
using Demo.DAl.Context;
using Demo.DAl.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositry
{
    public class DepartmentRepo:GenricRepo<Department>,IDepartmentRepo
    {
        public DepartmentRepo(WelcomeContext welcomeContext):base(welcomeContext) 
        {
            
        }
        public int GetDepartmentEmployees(int id)
        {

            var result = _welcomeContext.Employees.Count(emp => emp.DepartmentId == id);

            return result;
        }
    }
}
