using DiplomaProject.Models;

public class GeminiTestGenerationMock
{
    private readonly JsonDataService _jsonService;

    public GeminiTestGenerationMock(JsonDataService jsonService)
    {
        _jsonService = jsonService;
    }

    public async Task<GeminiResult> GenerateTests(string topic, int count)
    {
        try
        {
            Console.WriteLine($"TOPIC: {topic}");
            Console.WriteLine($"COUNT: {count}");

            var json = await _jsonService.GetRawJson();

            Console.WriteLine("RAW JSON FROM MOCK:");
            Console.WriteLine(json);

            var tests = _jsonService
                .DeserializeTests(json)
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