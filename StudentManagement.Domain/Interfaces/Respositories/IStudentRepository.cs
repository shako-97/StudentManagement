using StudentManagement.Domain.Entities;

namespace StudentManagement.Domain.Interfaces.Respositories
{
    public interface IStudentRepository
    {
        IQueryable<Student> Query();
        Task<Student?> GetByIdAsync(int id);
        Task AddStudentAsync(Student entity);
        void Delete(Student entity);
        void Update(Student entity);
    }
}
