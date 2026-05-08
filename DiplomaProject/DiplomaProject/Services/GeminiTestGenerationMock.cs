using DiplomaProject.Models;
using System.Text.Json;

public class GeminiTestGenerationMock
{
    private readonly IWebHostEnvironment _env;

    public GeminiTestGenerationMock(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<GeminiResult> GenerateTests(string topic, int count)
    {
        try
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "tests.json");

            Console.WriteLine($"MOCK PATH: {path}");
            Console.WriteLine($"TOPIC: {topic}");
            Console.WriteLine($"COUNT: {count}");

            if (!File.Exists(path))
            {
                return new GeminiResult
                {
                    Tests = new List<TestInfo>(),
                    RawJson = "FILE NOT FOUND: " + path
                };
            }

            var json = await File.ReadAllTextAsync(path);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var allTests = JsonSerializer.Deserialize<List<TestInfo>>(json, options)
                           ?? new List<TestInfo>();

            var tests = allTests
                .Take(count)
                .ToList();
            return new GeminiResult
            {
                Tests = tests,
                RawJson = json
            };
        }
        catch (Exception ex)
        {
            return new GeminiResult
            {
                Tests = new List<TestInfo>(),
                RawJson = $"ERROR: {ex.Message}"
            };
        }
    }
}