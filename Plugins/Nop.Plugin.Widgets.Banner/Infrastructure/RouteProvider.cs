using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Widgets.Banner.Infrastructure
{
    public class RouteProvider : IRouteProvider
    {
        public int Priority => 0;

        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute(name: PluginDefaults.ConfigurationRouteName,
                pattern: "Admin/Banner/Configure",
                defaults: new { controller = "Widgets", action = "Configure" });

            endpointRouteBuilder.MapControllerRoute(name: PluginDefaults.ListRouteName,
                pattern: "Admin/Banner/List",
                defaults: new { controller = "Widgets", action = "List" });
        }
    }
}
