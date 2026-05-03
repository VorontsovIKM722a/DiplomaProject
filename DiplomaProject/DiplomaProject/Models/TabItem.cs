using DiplomaProject.Models;

namespace DiplomaProject.Models
{
    public class TabItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = "Test";

        public TestState State { get; set; } = new TestState();
        public bool IsCompleted { get; set; } = false;
    }
}