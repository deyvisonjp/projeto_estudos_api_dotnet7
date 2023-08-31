﻿using PrimeiraApi.Model;

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
        public List<Employee> GetPerPage(int pageNumber, int pageQuantity)
        {
            return _context.Employees.Skip(pageNumber * pageQuantity).Take(pageQuantity).ToList();
        }


        public List<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public Employee Get(int id)
        {
            return _context.Employees.Find(id);
        }
    }
}
