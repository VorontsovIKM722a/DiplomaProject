namespace DiplomaProject.Models
{
    public class TestState
    {
        public List<TestInfo> Tests { get; set; } = new();
        public List<List<bool>> SelectedCheckbox { get; set; } = new();
        public List<int> SelectedRadio { get; set; } = new();

        public int CurrentQuestion { get; set; }
        public bool ShowResult { get; set; }
        public int Score { get; set; }
    }
}
