using BrimstoneMissionGenerator.Services;
using Microsoft.AspNetCore.Components;

namespace BrimstoneMissionGenerator.Pages
{
    public class AllMissionsBase : EngineBase
    {
#nullable disable //We're assuming that the runtime handles these
        [Inject] protected MissionService MissionService { get; set; }
#nullable restore

        protected AllMissionsBase()
        {
            PageTitle = "All Missions";
        }
    }
}
