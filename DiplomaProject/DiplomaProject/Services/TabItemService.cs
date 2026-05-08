using DiplomaProject.Models;
using DiplomaProject.Services;

public class TabItemService
{
    private readonly GeminiTestGeneration _testGeneration;

    public List<TabItem> Tabs { get; set; } = new();
    public string? ActiveTab { get; set; }

    public TabItemService(GeminiTestGeneration testGeneration)
    {
        _testGeneration = testGeneration;
    }

    // =========================
    // CREATE TAB (ASYNC + GEMINI)
    // =========================
    public async Task AddTabAsync()
    {
        var tab = new TabItem
        {
            Id = Guid.NewGuid().ToString(),
            Title = $"Test {Tabs.Count + 1}",
            State = CreateEmptyState()
        };

        Tabs.Add(tab);
        ActiveTab = tab.Id;
    }
    private TestState CreateEmptyState()
    {
        return new TestState
        {
            Tests = new List<TestInfo>(),
            SelectedCheckbox = new(),
            SelectedRadio = new(),
            CurrentQuestion = 0,
            ShowResult = false,
            Score = 0
        };
    }
    // =========================
    // REMOVE TAB
    // =========================
    public void RemoveTab()
    {
        if (ActiveTab == null)
            return;

        var tab = Tabs.FirstOrDefault(t => t.Id == ActiveTab);

        if (tab != null)
            Tabs.Remove(tab);

        ActiveTab = Tabs.LastOrDefault()?.Id;
    }

    // =========================
    // SELECT TAB
    // =========================
    public void SelectTab(string id)
    {
        ActiveTab = id;
    }

    // =========================
    // GET ACTIVE TAB
    // =========================
    public TabItem? GetActiveTab()
    {
        return Tabs.FirstOrDefault(t => t.Id == ActiveTab);
    }

    // =========================
    // CREATE TEST STATE (GEMINI)
    // =========================
    private async Task<TestState> CreateStateAsync(string topic, int count)
    {
        var result = await _testGeneration.GenerateTests(topic, count);
        var tests = result.Tests;

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