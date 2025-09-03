using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI1.DTO;
using WebAPI1.Interface;
using WebAPI1.Model;

namespace WebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartment department;
        private readonly IEmployee employee;

        public DepartmentController(IDepartment department,IEmployee employee)
        {
            this.department = department;
            this.employee = employee;
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            List<DeptListOfEmpDTO> listOfEmpDTOs = new List<DeptListOfEmpDTO>();
           var items= department.GetAll();
            foreach (var item in items)
            {
                DeptListOfEmpDTO deptListOfEmpDTO = new DeptListOfEmpDTO();
                deptListOfEmpDTO.Id=item.Id;
                deptListOfEmpDTO.Name=item.Name;
                deptListOfEmpDTO.EmployeesName=employee.GetAllById(item.Id);

                listOfEmpDTOs.Add(deptListOfEmpDTO);
            }
            return Ok(listOfEmpDTOs);
        }
        [HttpGet("{id:int}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            var item = department.GetById(id);
            return Ok(item);
        }
        
        [HttpGet("{name:alpha}")]
        [Authorize]
        public IActionResult GetByName(string name)
        {
            var item = department.GetByName(name);
            return Ok(item);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(Department dept)
        {
            department.Add(dept);
            department.Save();
            return CreatedAtAction("GetById",new {id= dept.Id},dept);
        }
        
        [HttpPut("{id:int}")]
        [Authorize]
        public IActionResult Update(int id,Department dept)
        {
            var item=department.GetById(id);
            if (item != null)
            {
                item.Name = dept.Name;
                item.ManagerName = dept.ManagerName;
                department.Update(item);
                department.Save();
                return NoContent();
            }
            return NotFound("Not Found");
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int id)
        {
            department.Delete(id);
            department.Save();
            return NoContent();
        }

    }
}
