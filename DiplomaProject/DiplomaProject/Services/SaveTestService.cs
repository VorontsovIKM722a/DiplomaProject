using DiplomaProject.Data;
using DiplomaProject.Models;
using DiplomaProject.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DiplomaProject.Services
{
    public class SaveTestService
    {
        private readonly ApplicationDbContext _db;

        public SaveTestService(ApplicationDbContext db)
        {
            _db = db;
        }

        // -------------------------
        // 1. SAVE СТРУКТУРИ ТЕСТУ
        // -------------------------
        public async Task SaveAsync(TestState state, string instanceId)
        {
            if (state == null)
                return;

            var tab = await _db.Tabs
                .Include(x => x.TestState)
                .FirstOrDefaultAsync(x => x.InstanceId == instanceId);

            if (tab == null)
            {
                tab = new TabItemEntity
                {
                    InstanceId = instanceId,
                    Title = state.Topic ?? "Test",
                    IsCompleted = false,
                    TestState = new TestStateEntity()
                };

                _db.Tabs.Add(tab);
            }

            tab.TestState ??= new TestStateEntity();

            tab.Title = state.Topic ?? tab.Title;

            tab.TestState.Mode = state.Mode.ToString();
            tab.TestState.Topic = state.Topic;
            tab.TestState.Instructions = state.Instructions;
            tab.TestState.Count = state.Count;
            tab.TestState.RawResponse = state.RawResponse;
            tab.TestState.UserJson = state.UserJson;
            tab.TestState.PdfPath = state.PdfPath ?? "";

            tab.TestState.TestsJson = JsonSerializer.Serialize(state.Tests);

            await _db.SaveChangesAsync();
        }

        // -------------------------
        // 2. SAVE РЕЗУЛЬТАТІВ (СПРОБА)
        // -------------------------
        public async Task SaveResultAsync(TestState state, string instanceId, int score)
        {
            if (state == null)
                return;

            var tab = await _db.Tabs
                .Include(x => x.TestState)
                    .ThenInclude(x => x.Attempts)
                        .ThenInclude(a => a.Answers)
                .FirstOrDefaultAsync(x => x.InstanceId == instanceId);

            if (tab?.TestState == null)
                return;

            var testState = tab.TestState;

            testState.Attempts ??= new List<TestAttemptEntity>();

            var attempt = new TestAttemptEntity
            {
                Score = score,
                CreatedAt = DateTime.UtcNow,
                Answers = new List<TestAnswerEntity>()
            };

            for (int i = 0; i < state.Tests.Count; i++)
            {
                var correct = state.Tests[i].CorrectAnswerIndexList ?? new List<int>();

                var userAnswer = state.SelectedRadio.Count > i
                    ? state.SelectedRadio[i]
                    : -1;

                bool isCorrect =
                    correct.Count == 1 &&
                    correct[0] == userAnswer;

                attempt.Answers.Add(new TestAnswerEntity
                {
                    QuestionIndex = i,
                    SelectedAnswerIndex = userAnswer,
                    IsCorrect = isCorrect
                });
            }

            testState.Attempts.Add(attempt);

            await _db.SaveChangesAsync();
        }
    }
}