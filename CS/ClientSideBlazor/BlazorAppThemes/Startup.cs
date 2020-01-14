using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorAppThemes
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services) {
			services.AddDevExpressBlazor();
		}

		public void Configure(IComponentsApplicationBuilder app) {
			app.AddComponent<App>("app");
		}
	}
}
