namespace DiplomaProject.Models.Entities
{
    public class TabItemEntity
    {
        public int Id { get; set; }
        public string InstanceId { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }

        public TestStateEntity TestState { get; set; }
    }
}
