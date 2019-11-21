using BrimstoneMissionGenerator.Models;
using BrimstoneMissionGenerator.Services;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrimstoneMissionGenerator.Pages
{
    public class IndexBase : EngineBase
    {
#nullable disable //We're assuming that the runtime handles these
        [Inject] protected MissionService MissionService { get; set; }

        //[Inject] protected NavigationManager Navigation { get; set; }
        [Inject] protected LocalStorage LocalStorage { get; set; }

#nullable restore

        protected IndexBase()
        {
            PageTitle = "";
        }

        override protected async Task InitializedAsync()
        {
            Sets = await MissionService.FindSetsAsync(IsConnected ? LocalStorage : null);
        }

        protected List<CheckList>? Sets { get; set; }
        bool m_DeferEvents;

        protected async void CheckboxClicked(CheckList item, object checkedValue)
        {
            if (m_DeferEvents)
                return;

            item.Available = (bool)checkedValue;

            await MissionService.SaveSetsAsync(LocalStorage, Sets);
        }

        protected async void CheckAllAsync()
        {
            if (Sets == null)
                return;

            m_DeferEvents = true;

            var action = true;

            if (Sets.All(x => x.Available))
                action = false;

            foreach (var item in Sets)
                item.Available = action;

            m_DeferEvents = false;

            await MissionService.SaveSetsAsync(LocalStorage, Sets);
        }
    }
}
