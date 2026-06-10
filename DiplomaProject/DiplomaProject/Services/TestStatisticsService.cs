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

        public List<TabStatisticsDto> BuildStatistics(List<TabItemEntity> tabs)
        {
            var result = new List<TabStatisticsDto>();

            foreach (var tab in tabs)
            {
                var attempts = tab.TestState?.Attempts?
                    .OrderByDescending(x => x.CreatedAt)
                    .ToList();

                if (attempts == null || attempts.Count == 0)
                    continue;

                // ✔ стабільна кількість питань (беремо з першої спроби)
                var maxScore = attempts.First().Answers?.Count ?? 0;

                if (maxScore == 0)
                    continue;

                // ✔ середній результат у %
                var avg = attempts.Average(x => (double)x.Score / maxScore)*10;

                var dto = new TabStatisticsDto
                {
                    Title = tab.Title,
                    AttemptsCount = attempts.Count,
                    AverageScore = avg,
                    MaxScore = maxScore,

                    Attempts = attempts.Select(a => new AttemptDto
                    {
                        Score = a.Score,
                        CreatedAt = a.CreatedAt,

                        Answers = a.Answers
                            .OrderBy(x => x.QuestionIndex)
                            .Select(ans => new AnswerDto
                            {
                                QuestionIndex = ans.QuestionIndex,
                                IsCorrect = ans.IsCorrect
                            })
                            .ToList()
                    }).ToList()
                };

                result.Add(dto);
            }

            return result;
        }
    }

    public class TabStatisticsDto
    {
        public string Title { get; set; } = null!;
        public int AttemptsCount { get; set; }

        // тепер це %
        public double AverageScore { get; set; }

        // кількість питань у тесті
        public int MaxScore { get; set; }

        public List<AttemptDto> Attempts { get; set; } = new();
    }

    public class AttemptDto
    {
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<AnswerDto> Answers { get; set; } = new();
    }

    public class AnswerDto
    {
        public int QuestionIndex { get; set; }
        public bool IsCorrect { get; set; }
    }
}