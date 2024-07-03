namespace FarmApp.Mobile;

public partial class Main
{
    public static event Func<Task>? OnBlazorInitialized;
    
    protected override void OnInitialized()
    {
        OnBlazorInitialized?.Invoke();
    }
}