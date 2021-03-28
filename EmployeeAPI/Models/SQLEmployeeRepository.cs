using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;

        public SQLEmployeeRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task Create(Employee employee)
        {
            await _context.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            Employee employee = await _context.Employees.FindAsync(Id);

            if (employee == null)
            {
                NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        private void NotFound()
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> GetEmployee(int Id)
        {
            return await _context.Employees.FindAsync(Id);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task Update(Employee updatedEmployee)
        {
            var employee = _context.Employees.Attach(updatedEmployee);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
