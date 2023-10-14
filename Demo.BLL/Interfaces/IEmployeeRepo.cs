using Demo.DAl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IEmployeeRepo : IGenericRepo<Employee>
    {
        IQueryable<Employee> GetByAddress(string Address);
        //IQueryable<Employee> SearchByName(string name);

    }
}
