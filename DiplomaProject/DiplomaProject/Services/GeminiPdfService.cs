using DiplomaProject.Models;
using Google.GenAI;
using Google.GenAI.Types;

public class GeminiPdfService
{
    private readonly Client _client;
    private readonly JsonDataService _jsonService;

    public GeminiPdfService(JsonDataService jsonService)
    {
        _client = new Client();
        _jsonService = jsonService;
    }

    public async Task<GeminiResult> GenerateTestsFromPdf(
     string filePath,
     int count,
     string instructions)
    {
        try
        {
            if (count <= 0)
                count = 5;

            if (count > 20)
                count = 20;

            var uploadedFile = await _client.Files.UploadAsync(
                filePath,
                new UploadFileConfig
                {
                    MimeType = "application/pdf"
                }
            );

            var contents = new List<Content>
        {
            new Content
            {
                Role = "user",
                Parts = new List<Part>
                {
                    new Part
                    {
                        FileData = new FileData
                        {
                            FileUri = uploadedFile.Uri,
                            MimeType = uploadedFile.MimeType
                        }
                    },

                    new Part
                    {
                        Text = $$"""
                        Згенеруй {{count}} тестових питань по цьому PDF.

                        ДОДАТКОВІ ІНСТРУКЦІЇ ВІД КОРИСТУВАЧА:
                        {{instructions}}

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
                        без markdown
                        без пояснень
                        рівно {{count}} питань
                        кілька правильних відповідей дозволені
                        """
                    }
                }
            }
        };

            var response = await _client.Models.GenerateContentAsync(
                model: "gemini-2.5-flash",
                contents: contents,
                config: new GenerateContentConfig
                {
                    ResponseMimeType = "application/json"
                }
            );

            var json = _jsonService.ExtractJson(response);

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