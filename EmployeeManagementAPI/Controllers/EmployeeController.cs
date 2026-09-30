using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]

        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)

        {
            var department = await _context.Departments.FindAsync(employee.DepartmentId);
            if(department == null)
            {
                return BadRequest("Department does not exist.");
            }
        
            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            return Ok(employee);

        }

        // Get

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _context.Employees
                .Include(e => e.Department)
                .ToListAsync();
            return Ok(employees);
        }

        // Get by DepartmentId

        [HttpGet("{id}")]

        public async Task<ActionResult<Employee>> GetEmployeeById(int id)

        {

            var employee = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)

            {

                return NotFound();

            }

            return Ok(employee);

        }

        // Put

        [HttpPut("{id}")]

        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)

        {

            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null)

            {

                return NotFound();

            }
            var department = await _context.Departments.FindAsync(employee.DepartmentId);
            if (department == null)
            {
                return BadRequest("Department does not exist.");
            }

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Email = employee.Email;
            existingEmployee.PhoneNumber = employee.PhoneNumber;
            existingEmployee.DateofJoining = employee.DateofJoining;
            existingEmployee.DepartmentId = employee.DepartmentId;
            existingEmployee.Salary = employee.Salary;

            await _context.SaveChangesAsync();
            return Ok(existingEmployee);

        }

        // Delete


        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteEmployee(int id)

        {

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)

            {

                return NotFound();

            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return NoContent();

        }



    }
}
