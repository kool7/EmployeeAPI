using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee 
                {
                    Id = 1,
                    Name = "Kuldeep Singh Chouhan",
                    Department = "Microsoft",
                    Designation = "Intern",
                    DOB = "7 June, 1997",
                    Email = "kschouhan714@gmail.com",
                });
        }
    }
}
