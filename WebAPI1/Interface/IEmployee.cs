using WebAPI1.Model;

namespace WebAPI1.Interface
{
    public interface IEmployee
    {
        public void Add(Employee employee);
        public void Update(Employee Employee);
        public void Delete(int id);
        public List<Employee> GetAll();
        public List<string> GetAllById(int DeptId);

        public Employee GetById(int id);
        public Employee GetByName(string name);

        public void Save();
    }
}
