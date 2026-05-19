using DiplomaProject.Data;
using DiplomaProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.Services
{
    public class EmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task AddAsync(Employee employee)
        {
            employee.Id = Guid.NewGuid();

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return;

            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();
        }
    }
}