using EmployeeAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public EmployeeController(IEmployeeRepository employeeRepository,
                                  IWebHostEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> ReadAll()
        {
            var employees = await _employeeRepository.GetEmployees();
            return Ok(employees);
        }

        [HttpGet("{Id}", Name = "ReadOne")]
        public async Task<ActionResult> ReadOne(int Id)
        {
            var employee = await _employeeRepository.GetEmployee(Id);

            if (employee != null)
            {
                return Ok(employee);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<Employee> AddEmployee(Employee employee)
        {
            _employeeRepository.Create(employee);
            return CreatedAtRoute(nameof(ReadOne), new { Id = employee.Id}, employee);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> RemoveEmployee(int Id)
        {
            await _employeeRepository.Delete(Id);
            return NoContent();
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateEmployee(int Id, Employee employee)
        {
            Employee employeeToUpdated = await _employeeRepository.GetEmployee(Id);

            if (employeeToUpdated != null)
            {
                employeeToUpdated.Name = employee.Name;
                employeeToUpdated.Email = employee.Email;
                employeeToUpdated.DOB = employee.DOB;
                employeeToUpdated.Designation = employee.Designation;
                employeeToUpdated.Department = employee.Department;
                employeeToUpdated.DOJ = employee.DOJ;
                employeeToUpdated.PhotoPath = employee.PhotoPath;

                await _employeeRepository.Update(employeeToUpdated);
                return NoContent();
            }

            return NotFound();
        }

        [HttpPost("SaveFile")]
        public async Task<JsonResult> UploadPhoto()
        {
            var httpRequest = Request.Form;
            var postedFile = httpRequest.Files[0];
            string fileName = postedFile.FileName;
            var physicalPath = _hostingEnvironment.ContentRootPath + "/Photos/" + fileName;

            using(var stream = new FileStream(physicalPath, FileMode.Create))
            {
                await postedFile.CopyToAsync(stream);
            }

            return new JsonResult(fileName);
        }
    }
} 
