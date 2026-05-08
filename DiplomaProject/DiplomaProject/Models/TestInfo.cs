using System.Text.Json.Serialization;

namespace DiplomaProject.Models
{
    public class TestInfo
    {
        [JsonPropertyName("taskDescription")]
        public string TaskDescription { get; set; }

        [JsonPropertyName("taskAnswers")]
        public List<string> TaskAnswers { get; set; } = new();

        [JsonPropertyName("correctAnswerIndexList")]
        public List<int> CorrectAnswerIndexList { get; set; } = new();

        public TestInfo() { }

        public TestInfo(string taskDescription, List<string> taskAnswers, List<int> correctAnswerIndexList)
        {
            TaskDescription = taskDescription;
            TaskAnswers = taskAnswers ?? new();
            CorrectAnswerIndexList = correctAnswerIndexList ?? new();
        }
    }
}