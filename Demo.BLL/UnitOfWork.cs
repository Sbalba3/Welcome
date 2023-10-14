using Demo.BLL.Interfaces;
using Demo.BLL.Repositry;
using Demo.DAl.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly WelcomeContext _welcomeContext;
        public UnitOfWork(WelcomeContext welcomeContext)
        {
            EmployeeRepo=new EmployeeRepo(welcomeContext);
            DepartmentRepo=new DepartmentRepo(welcomeContext);
            _welcomeContext = welcomeContext;
            
        }
        public IEmployeeRepo EmployeeRepo { get; set; }
        public IDepartmentRepo DepartmentRepo { get; set; }
        public async Task Save()
        {
           await _welcomeContext.SaveChangesAsync();
        }
        public void Dispose()
           => _welcomeContext.Dispose();
        
           
    }
}
