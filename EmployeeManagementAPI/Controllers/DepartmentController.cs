using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        // Post

        [HttpPost]
        public IActionResult AddDepartment(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
            return Ok(department);
        }

        // Get

        [HttpGet]
        public IActionResult GetDepartments()
        {
            return Ok(_context.Departments.ToList());
        }

        // Get with DepartmentId

        [HttpGet("{id}")]
        public IActionResult GetDepartment(int id)
        {
            var department = _context.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        // Put

        [HttpPut("{id}")]
        public IActionResult UpdateDepartment(int id, Department department)
        {
            var existingDepartment = _context.Departments.Find(id);
            if (existingDepartment == null)
            {
                return NotFound();
            }
            existingDepartment.Name = department.Name;
            existingDepartment.Location = department.Location;
            _context.SaveChanges();
            return Ok(existingDepartment);
        }

        // Delete

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteDepartment(int id)

        {

            var department = await _context.Departments.FindAsync(id);

            if (department == null)

            {

                return NotFound();

            }

            _context.Departments.Remove(department);

            await _context.SaveChangesAsync();

            return NoContent();

        }


    }
}
