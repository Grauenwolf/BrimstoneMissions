using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BrimstoneMissionGenerator.Pages
{
    public class EngineBase : ComponentBase
    {
#nullable disable
        [Inject] protected IJSRuntime JSRuntime { get; set; }
#nullable restore

        protected string? PageTitle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is connected.
        /// </summary>
        /// <value><c>true</c> if this instance is connected; <c>false</c> if it is pre-rendering.</value>
        protected bool IsConnected { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                var result = await JSRuntime.InvokeAsync<bool>("isPreRendering");
                IsConnected = true;
            }
            catch (NullReferenceException)
            {
            }
        }

        async protected override Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeVoidAsync("setTitle", PageTitle);
            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
