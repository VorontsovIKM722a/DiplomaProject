namespace DiplomaProject.Models.Entities
{
    public class TestResultEntity
    {
        public int Id { get; set; }

        public int TestStateId { get; set; }
        public TestStateEntity TestState { get; set; }

        public int Score { get; set; }

        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

        public List<TestAnswerResultEntity> Answers { get; set; } = new();
    }
}