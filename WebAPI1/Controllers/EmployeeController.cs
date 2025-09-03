using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI1.Interface;
using WebAPI1.Model;

namespace WebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee employee;

        public EmployeeController(IEmployee employee)
        {
            this.employee = employee;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = employee.GetAll();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var item = employee.GetById(id);
            return Ok(item);
        }

        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            var item = employee.GetByName(name);
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Add(Employee emp)
        {
            employee.Add(emp);
            employee.Save();
            return CreatedAtAction("GetById", new { id = emp.Id }, emp);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Employee emp)
        {
            var item = employee.GetById(id);
            if (item != null)
            {
                item.Name = emp.Name;
                item.Address = emp.Address;
                employee.Update(item);
                employee.Save();
                return NoContent();
            }
            return NotFound("Not Found");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            employee.Delete(id);
            employee.Save();
            return NoContent();
        }

    }
}
