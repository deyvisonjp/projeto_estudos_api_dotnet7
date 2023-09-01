using Microsoft.EntityFrameworkCore;
using PrimeiraApi.Model;

namespace PrimeiraApi.Infra
{
    public class EmployeeRepository : IEmployeeRepoository
    {
        private readonly ConnectionContext _context = new ConnectionContext();
        public void Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }
        public async Task<List<Employee>> GetPerPageAsync(int pageNumber, int pageQuantity)
        { 
            var employees = await _context.Employees.Skip(pageNumber * pageQuantity).Take(pageQuantity).AsNoTracking().ToListAsync();
            return employees;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            var employees = await _context.Employees.AsNoTracking().ToListAsync();
            return employees;
        }

        public Employee Get(int id)
        {
            return _context.Employees.Find(id);
        }

        public int GetTotalCount()
        {
            return _context.Employees.Count();
        }

    }
}
