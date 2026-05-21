namespace DiplomaProject.Models.Entities
{
    public class TestAnswerResultEntity
    {
        public int Id { get; set; }

        public int TestResultId { get; set; }
        public TestResultEntity TestResult { get; set; }

        public int QuestionIndex { get; set; }

        public int? SelectedAnswerIndex { get; set; }

        public bool IsCorrect { get; set; }
    }
}