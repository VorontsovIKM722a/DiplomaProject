using DiplomaProject.Models;
using System.Text.Json;

public class JsonDataService
{
    private readonly IWebHostEnvironment _env;

    public JsonDataService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<List<TestInfo>> GetTests()
    {
        var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            "Data",
            "tests.json");

        if (!File.Exists(path))
            return new List<TestInfo>();

        var json = await File.ReadAllTextAsync(path);

        return DeserializeTests(json);
    }

    public async Task<string> GetRawJson()
    {
        var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            "Data",
            "tests.json");

        if (!File.Exists(path))
            return "";

        return await File.ReadAllTextAsync(path);
    }

    public string ExtractJson(dynamic response)
    {
        return response?.Candidates?[0]?.Content?.Parts?[0]?.Text ?? "";
    }

    public List<TestInfo> DeserializeTests(string json)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(json))
                return new List<TestInfo>();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var tests = JsonSerializer.Deserialize<List<TestInfo>>(json, options);

            return tests?.Select(t => new TestInfo(
                t.TaskDescription ?? "",
                t.TaskAnswers ?? new List<string>(),
                t.CorrectAnswerIndexList ?? new List<int>()
            )).ToList()
            ?? new List<TestInfo>();
        }
        catch (Exception ex)
        {
            Console.WriteLine("PARSE ERROR: " + ex.Message);
            Console.WriteLine(json);

            return new List<TestInfo>();
        }
    }
}