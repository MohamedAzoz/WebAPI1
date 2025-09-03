using WebAPI1.Model;

namespace WebAPI1.Interface
{
    public interface IDepartment
    {
        public void Add(Department department);
        public void Update(Department department);
        public void Delete(int id);
        public List<Department> GetAll();

        public Department GetById(int id);
        public Department GetByName(string name);

        public void Save();

    }
}
