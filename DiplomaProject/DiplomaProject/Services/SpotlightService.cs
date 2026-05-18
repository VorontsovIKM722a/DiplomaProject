namespace DiplomaProject.Services;

public class SpotlightService
{
    public bool Enabled { get; private set; } = true;
    public int Radius { get; private set; } = 120;

    public event Action? OnChange;

    public void Toggle()
    {
        Enabled = !Enabled;
        Notify();
    }

    public void SetRadius(int radius)
    {
        Radius = radius;
        Notify();
    }

    private void Notify()
    {
        OnChange?.Invoke();
    }
}