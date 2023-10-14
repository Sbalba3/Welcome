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
    public class GenricRepo<T>:IGenericRepo<T> where T : class
    {
        private protected readonly WelcomeContext _welcomeContext;
        public GenricRepo(WelcomeContext welcomeContext)
        {
            _welcomeContext = welcomeContext;

        }
        public async Task< IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
             return (IEnumerable<T>)await _welcomeContext.Employees.Include(x=>x.Department).ToListAsync();
            }
            return await _welcomeContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetById(int id)
        {
            return await _welcomeContext.Set<T>().FindAsync (id);
        }
        public async Task Add(T item)
        {
           await _welcomeContext.Set<T>().AddAsync(item);
        }
        public void Update(T item)
        {
            _welcomeContext.Entry(item).State = EntityState.Modified;
        }
        public void Delete(T item)
        {
             _welcomeContext.Set<T>().Remove(item);
        }
        
    }
}
