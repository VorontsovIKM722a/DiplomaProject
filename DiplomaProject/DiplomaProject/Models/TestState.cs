using DiplomaProject.Models;

public class TestState
{
    public string Topic { get; set; }
    public int Count { get; set; } = 5;

    public List<TestInfo> Tests { get; set; } = new();

    public List<List<bool>> SelectedCheckbox { get; set; } = new();
    public List<int> SelectedRadio { get; set; } = new();

    public int CurrentQuestion { get; set; }

    public bool ShowResult { get; set; }
    public int Score { get; set; }

    public bool IsGenerated { get; set; }
    public string RawResponse { get; set; }
}
