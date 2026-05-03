using DiplomaProject.Models;
using DiplomaProject.Services;

public class TabItemService
{
    private readonly TestGeneration _testGeneration;

    public List<TabItem> Tabs { get; set; } = new();
    public string? ActiveTab { get; set; }

    public TabItemService(TestGeneration testGeneration)
    {
        _testGeneration = testGeneration;
    }

    public void AddTab()
    {
        var tab = new TabItem
        {
            Title = $"Test {Tabs.Count + 1}",
            State = CreateState()
        };

        Tabs.Add(tab);
        ActiveTab = tab.Id;
    }

    public void SelectTab(string id)
    {
        ActiveTab = id;
    }

    public TabItem? GetActiveTab()
    {
        return Tabs.FirstOrDefault(t => t.Id == ActiveTab);
    }

    private TestState CreateState()
    {
        var tests = _testGeneration.GetSampleTests();

        return new TestState
        {
            Tests = tests,
            SelectedCheckbox = tests
                .Select(t => t.TaskAnswers.Select(_ => false).ToList())
                .ToList(),
            SelectedRadio = tests
                .Select(_ => -1)
                .ToList(),
            CurrentQuestion = 0,
            ShowResult = false,
            Score = 0
        };
    }
}