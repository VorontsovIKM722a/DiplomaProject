using DiplomaProject.Data;
using DiplomaProject.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.Services
{
    public class TestStatisticsService
    {
        private readonly ApplicationDbContext _db;

        public TestStatisticsService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<TabItemEntity>> GetStatisticsAsync()
        {
            return await _db.Tabs
                .Include(t => t.TestState)
                    .ThenInclude(s => s.Attempts)
                        .ThenInclude(a => a.Answers)
                .OrderBy(t => t.Title)
                .ToListAsync();
        }
    }
}