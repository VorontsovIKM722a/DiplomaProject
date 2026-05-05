using Google.GenAI;

public class GeminiService
{
    private readonly Client _client;

    public GeminiService()
    {
        _client = new Client(); // бере API key з GOOGLE_API_KEY
    }

    public async Task<string> Generate(string prompt)
    {
        try
        {
            var response = await _client.Models.GenerateContentAsync(
                model: "gemini-2.5-flash",
                contents: prompt
            );

            return response?.Candidates?[0]?.Content?.Parts?[0]?.Text
                   ?? "EMPTY RESPONSE";
        }
        catch (Exception ex)
        {
            return $"ERROR: {ex.Message}";
        }
    }
}