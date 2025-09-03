using WebAPI1.Model;

namespace WebAPI1.DTO
{
    public class DeptListOfEmpDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        

        public List<string>? EmployeesName { get; set; }
    }
}
