using Microsoft.AspNetCore.Components;
using FarmApp.Mobile.Pages.Main.TabPanel.Models;

namespace FarmApp.Mobile.Pages.Main.TabPanel.Tab;

public partial class TabComponent
{
    [Parameter] public required TabModel TabModel { get; set; }
    [Parameter] public bool Active { get; set; }
    [Parameter] public EventCallback<TabModel> TabClicked { get; set; }

}