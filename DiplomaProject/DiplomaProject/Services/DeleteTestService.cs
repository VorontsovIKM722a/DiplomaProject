using DiplomaProject.Data;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.Services
{
    public class DeleteTestService
    {
        private readonly AppDbContext _db;

        public DeleteTestService(AppDbContext db)
        {
            _db = db;
        }

        public async Task DeleteByInstanceIdAsync(string instanceId)
        {
            var tab = await _db.Tabs
                .Include(t => t.TestState)
                .FirstOrDefaultAsync(t => t.InstanceId == instanceId);

            if (tab == null)
                return;

            if (tab.TestState != null)
            {
                _db.TestStates.Remove(tab.TestState);
            }

            _db.Tabs.Remove(tab);

            await _db.SaveChangesAsync();
        }
    }
}