using DiplomaProject.Models;

public class GeminiTestGenerationLocal
{
    private readonly JsonDataService _jsonService;

    public GeminiTestGenerationLocal(JsonDataService jsonService)
    {
        _jsonService = jsonService;
    }

    public async Task<GeminiResult> GenerateTests(string topic, int count, string instructions)
    {
        try
        {
            topic ??= "mock topic";
            instructions ??= "";

            if (count <= 0)
                count = 5;

            if (count > 20)
                count = 20;

            Console.WriteLine($"TOPIC: {topic}");
            Console.WriteLine($"COUNT: {count}");
            Console.WriteLine($"INSTRUCTIONS: {instructions}");

            var json = await _jsonService.GetRawJson();

            Console.WriteLine("RAW JSON FROM MOCK:");
            Console.WriteLine(json);

            var tests = _jsonService
                .DeserializeTests(json)
                .Take(count)
                .ToList();

            if (!string.IsNullOrWhiteSpace(instructions))
            {
                Console.WriteLine("Applying mock instructions (no real AI, just simulation)");
            }

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