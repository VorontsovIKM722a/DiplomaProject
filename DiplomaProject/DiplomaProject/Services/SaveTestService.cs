using DiplomaProject.Data;
using DiplomaProject.Models;
using DiplomaProject.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DiplomaProject.Services
{
    public class SaveTestService
    {
        private readonly AppDbContext _db;

        public SaveTestService(AppDbContext db)
        {
            _db = db;
        }

        public async Task SaveAsync(TestState state, string instanceId)
        {
            if (state == null || state.Tests == null)
                return;

            var entity = await _db.Tabs
                .Include(x => x.TestState)
                .FirstOrDefaultAsync(x => x.InstanceId == instanceId);

            if (entity == null)
            {
                entity = new TabItemEntity
                {
                    InstanceId = instanceId,
                    Title = state.Topic,
                    IsCompleted = false
                };

                _db.Tabs.Add(entity);
            }

            entity.Title = state.Topic;

            if (entity.TestState == null)
            {
                entity.TestState = new TestStateEntity();
            }

            entity.TestState.Mode = state.Mode.ToString();
            entity.TestState.Topic = state.Topic;
            entity.TestState.Instructions = state.Instructions;
            entity.TestState.Count = state.Count;
            entity.TestState.RawResponse = state.RawResponse;
            entity.TestState.UserJson = state.UserJson;
            entity.TestState.PdfPath = state.PdfPath ?? "";

            entity.TestState.TestsJson =
                JsonSerializer.Serialize(state.Tests);

            await _db.SaveChangesAsync();
        }
    }
}