using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employee;
        public MockEmployeeRepository()
        {
            _employee = new List<Employee>()
            {
                new Employee{ Id = 1, Name = "Kuldeep Singh Chouhan", Email = "kuldeep.si@cisinlabs.com", Department = "Microsoft", Designation = "Intern", DOB = "7 June, 1997"},
                new Employee{ Id = 2, Name = "Sagar Soni", Email = "Sagar.so@cisinlabs.com", Department = "Microsoft", Designation = "Intern", DOB = "6 June, 1999"},
                new Employee{ Id = 3, Name = "Piyush Thakur", Email = "Piyush@cisinlabs.com", Department = "Microsoft", Designation = "Intern", DOB = "22 Feb, 1999"},
                new Employee{ Id = 4, Name = "Hitesh Joshi", Email = "Hitesh.@cisinlabs.com", Department = "Microsoft", Designation = "Intern", DOB = "5 March, 1999"}
            };
        }

        public async Task Create(Employee employee)
        {
            employee.Id = _employee.Max(e => e.Id) + 1;
            _employee.Add(employee);
        }

        public async Task Delete(int Id)
        {
            var employee = _employee.FirstOrDefault(e => e.Id == Id);
            _employee.Remove(employee);
        }

        public async Task<Employee> GetEmployee(int Id)
        {
           return _employee.FirstOrDefault(e => e.Id == Id);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return _employee;
        }

        public async Task Update(Employee updatedEmployee)
        {
            throw new NotImplementedException();
        }
    }
}
