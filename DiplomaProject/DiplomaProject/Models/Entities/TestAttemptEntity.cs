public class TestAttemptEntity
{
    public int Id { get; set; }
    
    public int Score { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<TestAnswerEntity> Answers { get; set; } = new();
}