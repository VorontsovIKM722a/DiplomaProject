using DiplomaProject.Models;
using Google.GenAI;
using Google.GenAI.Types;

public class GeminiTestGeneration
{
    private readonly Client _client;
    private readonly JsonDataService _jsonService;

    public GeminiTestGeneration(JsonDataService jsonService)
    {
        _client = new Client();
        _jsonService = jsonService;
    }

    public async Task<GeminiResult> GenerateTests(string topic, int count)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(topic))
                topic = "загальні знання";

            if (count <= 0)
                count = 5;

            if (count > 20)
                count = 20;

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

            var json = _jsonService.ExtractJson(response);

            Console.WriteLine("RAW JSON FROM GEMINI:");
            Console.WriteLine(json);

            var tests = _jsonService.DeserializeTests(json);

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