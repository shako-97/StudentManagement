using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces.Respositories;
using StudentManagement.Infrastructure.DataAccess;

namespace StudentManagement.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Student> Query()
        {
            return _context.Students.AsQueryable();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task AddStudentAsync(Student entity)
        {
            await _context.Students.AddAsync(entity);
        }

        public void Delete(Student entity)
        {
            _context.Students.Remove(entity);
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);
        }
    }
}
