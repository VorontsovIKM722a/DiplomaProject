using DiplomaProject.Data;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.Services
{
    public class DeleteTestService
    {
        private readonly ApplicationDbContext _db;

        public DeleteTestService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task DeleteByInstanceIdAsync(string instanceId)
        {
            var tab = await _db.Tabs
                .Include(t => t.TestState)
                    .ThenInclude(ts => ts.Attempts)
                        .ThenInclude(a => a.Answers)
                .FirstOrDefaultAsync(t => t.InstanceId == instanceId);

            if (tab == null)
                return;

            if (tab.TestState != null)
            {
                // 1. Видаляємо відповіді всіх спроб
                foreach (var attempt in tab.TestState.Attempts)
                {
                    if (attempt.Answers.Any())
                    {
                        _db.Set<TestAnswerEntity>()
                            .RemoveRange(attempt.Answers);
                    }
                }

                // 2. Видаляємо всі спроби
                if (tab.TestState.Attempts.Any())
                {
                    _db.Set<TestAttemptEntity>()
                        .RemoveRange(tab.TestState.Attempts);
                }

                // 3. Видаляємо сам тест
                _db.TestStates.Remove(tab.TestState);
            }

            // 4. Видаляємо вкладку
            _db.Tabs.Remove(tab);

            await _db.SaveChangesAsync();
        }
    }
}