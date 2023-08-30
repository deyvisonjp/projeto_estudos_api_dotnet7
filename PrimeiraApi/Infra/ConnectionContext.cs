using Microsoft.EntityFrameworkCore;
using PrimeiraApi.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace PrimeiraApi.Infra
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=DESKTOP-LRE13PI\\SQLEXPRESS;Initial Catalog=DB_Employee;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    }
}
