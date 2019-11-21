using BrimstoneMissionGenerator.Models;
using BrimstoneMissionGenerator.Services;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BrimstoneMissionGenerator.Pages
{
    public class MissionViewBase : EngineBase
    {
#nullable disable //We're assuming that the runtime handles these
        [Inject] protected MissionService MissionService { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }
        [Inject] protected LocalStorage LocalStorage { get; set; }
#nullable restore

        protected MissionViewBase()
        {
        }

        [Parameter]
        public string? Id { get; set; }

        [Parameter]
        public string? Number { get; set; }

        public MissionPicker? Model { get; set; }

        protected override async Task ParametersSetAsync()
        {
            if (Id == null && Number == null)
            {
                Model = await MissionService.GenerateMissionAsync(IsConnected ? LocalStorage : null);
                Navigation.NavigateTo("/mission/" + Model.MissionsSet.Id + "/" + Model.Mission.Number);
                return;
            }

            if (!int.TryParse(Id, out var missionSet))
            {
                //TODO: go home
                return;
            }
            if (!int.TryParse(Number, out var missionNumber))
            {
                //TODO: go home
                return;
            }

            Model = await MissionService.GenerateMissionAsync(IsConnected ? LocalStorage : null, missionSet, missionNumber);

            if (Model == null)
            {
                //TODO: go home
                return;
            }

            PageTitle = Model.Mission.Name;
        }
    }
}
