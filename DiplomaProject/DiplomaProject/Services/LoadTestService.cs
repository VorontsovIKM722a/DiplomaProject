using DiplomaProject.Data;
using DiplomaProject.Models;
using DiplomaProject.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DiplomaProject.Services
{
    public class LoadTestService
    {
        private readonly AppDbContext _db;

        public LoadTestService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<TabItem>> LoadTabsAsync()
        {
            var tabs = await _db.Tabs
                .Include(x => x.TestState)
                .ToListAsync();

            return tabs.Select(t => new TabItem
            {
                Id = t.InstanceId,
                Title = t.Title,
                IsCompleted = t.IsCompleted,

                State = new TestState
                {
                    Topic = t.TestState?.Topic ?? "",
                    Instructions = t.TestState?.Instructions ?? "",
                    Count = t.TestState?.Count ?? 0,
                    RawResponse = t.TestState?.RawResponse ?? "",
                    UserJson = t.TestState?.UserJson ?? "",
                    PdfPath = t.TestState?.PdfPath ?? "",

                    Tests = string.IsNullOrWhiteSpace(t.TestState?.TestsJson)
                    ? new List<TestInfo>()
                    : JsonSerializer.Deserialize<List<TestInfo>>(t.TestState.TestsJson) ?? new List<TestInfo>()
                }
            }).ToList();
        }
    }
}