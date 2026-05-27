using DiplomaProject.Models;
using DiplomaProject.Services;

public class TabItemService
{
    private readonly GeminiTestGeneration _testGeneration;
    private readonly DeleteTestService _deleteTestService;

    
    public event Action? OnChange;

    public List<TabItem> Tabs { get; set; } = new();
    public string? ActiveTab { get; set; }

    public TabItemService(
        GeminiTestGeneration testGeneration,
        DeleteTestService deleteTestService)
    {
        _testGeneration = testGeneration;
        _deleteTestService = deleteTestService;
    }

   
    public void NotifyStateChanged() => OnChange?.Invoke();

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
        NotifyStateChanged();
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

    public async Task RemoveTabAsync()
    {
        if (ActiveTab == null)
            return;

        var tab = Tabs.FirstOrDefault(t => t.Id == ActiveTab);

        if (tab != null)
        {
            await _deleteTestService.DeleteByInstanceIdAsync(tab.Id);
            Tabs.Remove(tab);
        }

        ActiveTab = Tabs.LastOrDefault()?.Id;

      
        NotifyStateChanged();
    }

    public void SelectTab(string id)
    {
        ActiveTab = id;

       
        NotifyStateChanged();
    }

    public TabItem? GetActiveTab()
    {
        return Tabs.FirstOrDefault(t => t.Id == ActiveTab);
    }

    public void RestartActiveTab()
    {
        var tab = GetActiveTab();

        if (tab?.State == null)
            return;

        var state = tab.State;

        state.CurrentQuestion = 0;
        state.ShowResult = false;
        state.Score = 0;
        state.IsSaved = false;

        state.SelectedRadio ??= new();
        state.SelectedCheckbox ??= new();

        state.CurrentQuestion = 0;

        NotifyStateChanged();
    }
}