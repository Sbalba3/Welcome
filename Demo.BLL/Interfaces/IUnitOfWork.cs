using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
   public interface IUnitOfWork:IDisposable
    {
        public IEmployeeRepo EmployeeRepo { get; set; }
        public IDepartmentRepo DepartmentRepo { get; set; }
       Task Save();
    }
}
