namespace DiplomaProject.Models.Entities
{
    public class TestStateEntity
    {
        public int Id { get; set; }

        public int TabItemId { get; set; }
        public TabItemEntity TabItem { get; set; }

        public string Mode { get; set; }
        public string Topic { get; set; }
        public string Instructions { get; set; }
        public int Count { get; set; }

        public string RawResponse { get; set; }
        public string UserJson { get; set; }
        public string PdfPath { get; set; }
        public List<TestAttemptEntity> Attempts { get; set; } = new();
        public string TestsJson { get; set; }
    }
}
