using DiplomaProject.Models;
using Google.GenAI;
using Google.GenAI.Types;
using System.Text.Json;

public class GeminiTestGeneration
{
    private readonly Client _client;

    public GeminiTestGeneration()
    {
        _client = new Client();
    }

    public async Task<GeminiResult> GenerateTests(string topic, int count)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(topic))
                topic = "загальні знання";

            if (count <= 0) count = 5;
            if (count > 20) count = 20;

            var prompt = $$"""
                Згенеруй {{count}} тестових питань по темі: {{topic}}.

                Формат відповіді строго JSON:

                [
                  {
                    "taskDescription": "string",
                    "taskAnswers": ["a", "b", "c", "d"],
                    "correctAnswerIndexList": [0]
                  }
                ]

                Вимоги:
                тільки JSON
                без пояснень
                без markdown
                рівно {{count}} питань
                кілька завдань мають бути з кількома правильними відповідями
                """;
            var response = await _client.Models.GenerateContentAsync(
                model: "gemini-2.5-flash",
                contents: prompt,
                config: new GenerateContentConfig
                {
                    ResponseMimeType = "application/json"
                }
            );

            var json = ExtractJson(response);

            Console.WriteLine("RAW JSON FROM GEMINI:");
            Console.WriteLine(json);

            var tests = Deserialize(json);

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

    private string ExtractJson(dynamic response)
    {
        return response?.Candidates?[0]?.Content?.Parts?[0]?.Text ?? "";
    }

    private List<TestInfo> Deserialize(string json)
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