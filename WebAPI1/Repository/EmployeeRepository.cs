using WebAPI1.Data;
using WebAPI1.Interface;
using WebAPI1.Model;

namespace WebAPI1.Repository
{
    public class EmployeeRepository : IEmployee
    {
        private readonly AppDbContext context;

        public EmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void Add(Employee employee)
        {
            context.Employees.Add(employee);
        }

        public void Update(Employee Employee)
        {
            context.Update(Employee);
        }

        public void Delete(int id)
        {
            var item=GetById(id);
            context.Employees.Remove(item);
        }

        public List<Employee> GetAll()
        {
            return context.Employees.ToList();
        }

        public Employee GetById(int id)
        {
            var item=context.Employees.FirstOrDefault(x=>x.Id==id);
            if (item!=null)
            {
                return item;
            }
            throw new Exception("Not Found");
        }

        public Employee GetByName(string name)
        {
            var item = context.Employees.FirstOrDefault(x => x.Name == name);
            if (item != null)
            {
                return item;
            }
            throw new Exception("Not Found");
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public List<string> GetAllById(int DeptId)
        {
            return context.Employees.Where(x=>x.DepartmentId==DeptId).Select(x=>x.Name).ToList();
        }
    }
}
