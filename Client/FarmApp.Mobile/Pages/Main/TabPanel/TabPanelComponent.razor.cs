using FarmApp.Mobile.Pages.Main.TabPanel.Models;

namespace FarmApp.Mobile.Pages.Main.TabPanel;

public partial class TabPanelComponent
{
    private const string TabsHeight = "60px";
    private readonly string _key = $"_key_{Guid.NewGuid()}";

    private TabModel? _selectedTab = null;
    private bool _show = false;
    
    private readonly List<TabModel> _tabModels = new List<TabModel>
    {
        new() { Name = "home" },
        new() { Name = "fields" },
        new() { Name = "subscription" },
        new() { Name = "user" }
    };

    protected override async Task OnInitializedAsync()
    {
        _selectedTab = _tabModels.FirstOrDefault();
        await Task.Delay(500);
        
        _show = true;
        StateHasChanged();
    }

    private void OnTabClicked(TabModel model)
    {
        _selectedTab = model;
        StateHasChanged();
    }
}