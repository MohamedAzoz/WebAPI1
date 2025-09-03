using WebAPI1.Data;
using WebAPI1.Interface;
using WebAPI1.Model;

namespace WebAPI1.Repository
{
    public class DepartmentRepository : IDepartment
    {
        private readonly AppDbContext context;

        public DepartmentRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void Add(Department department)
        {
            context.Add(department);
        }

        public void Update(Department department)
        {
            context.Update(department);
        }

        public void Delete(int id)
        {
            var item =GetById(id);
            context.Departments.Remove(item);
        }

        public List<Department> GetAll()
        {
            return context.Departments.ToList();
        }

        public Department GetById(int id)
        {
            var item = context.Departments.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                return item;
            }
            throw new Exception("Not Found");
        }

        public Department GetByName(string name)
        {
            var item = context.Departments.FirstOrDefault(x => x.Name == name);
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

       
    }
}
