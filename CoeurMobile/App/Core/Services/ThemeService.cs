namespace CoeurMobile.App.Core.Services;

public class ThemeService
{
    public bool IsDarkMode { get; private set; } = true;

    public event Action? OnChange;

    public void SetDarkMode(bool isDarkMode)
    {
        if (IsDarkMode == isDarkMode)
        {
            return;
        }

        IsDarkMode = isDarkMode;
        OnChange?.Invoke();
    }

    public void Toggle()
    {
        SetDarkMode(!IsDarkMode);
    }
}
