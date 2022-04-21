using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Core.Domain.Cms;
using Nop.Core.Infrastructure;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Plugins;
using Nop.Web.Framework;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Widgets.Banner
{
    public class BannerPlugin : BasePlugin, IWidgetPlugin, IAdminMenuPlugin
    {
        #region field

        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly INopFileProvider _fileProvider;
        private readonly WidgetSettings _widgetSettings;

        #endregion

        #region CTOR

        public BannerPlugin(ILocalizationService localizationService, IPictureService pictureService, ISettingService settingService, IWebHelper webHelper, INopFileProvider nopFileProvider, WidgetSettings widgetSettings)
        {
            _localizationService = localizationService;
            _pictureService = pictureService;   
            _settingService = settingService;
            _webHelper = webHelper;
            _fileProvider = nopFileProvider;
            _widgetSettings = widgetSettings;
        }

        #endregion

        #region METHODS

        public bool HideInWidgetList => false;

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "WidgetsBanner";
        }
        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsBanner/Configure";
        }
        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>widget zones</returns>
        public async Task<IList<string>> GetWidgetZonesAsync()
        {
            var list = new List<string>();
            list.AddRange(await PluginDefaults.AllowedWidgetsZones());
            list.Add(PluginDefaults.BannerFilters);
            return list;
        }

        public async Task ManageSiteMapAsync(SiteMapNode rootNode)
        {
            var mainMenuItem = new SiteMapNode()
            {
                SystemName = "WidgetsBanner",
                Title = await _localizationService.GetResourceAsync("Plugins.Widgets.Banner"),
                ControllerName = "",
                ActionName = "",
                IconClass = "fa fa-dot-circle-o",
                Visible = true,
                RouteValues = new RouteValueDictionary() { { "area", AreaNames.Admin} }
            };

            var listMenus = new List<SiteMapNode>();
            var menuItem = new SiteMapNode();
            listMenus.Add(menuItem);

            menuItem = new SiteMapNode()
            {
                SystemName = "ConfigureWidgetsBanner",
                Title = await _localizationService.GetResourceAsync("Plugins.Widgets.Banner.BannerConfigure"),
                ControllerName = "WidgetsBanner",
                ActionName = "Configure",
                IconClass = "fa fa-dot-circle-o",
                Visible = true,
                RouteValues = new RouteValueDictionary() { { "area", AreaNames.Admin } }
            };
            listMenus.Add(menuItem);

            menuItem = new SiteMapNode()
            {
                SystemName = "WidgetsBannerList",
                Title = await _localizationService.GetResourceAsync("Plugins.Widgets.Banner.BannerList"),
                ControllerName = "WidgetsBanner",
                ActionName = "Banner",
                IconClass = "fa fa-dot-circle-o",
                Visible = true,
                RouteValues = new RouteValueDictionary() { { "area", AreaNames.Admin } }
            };
            listMenus.Add(menuItem);

            var pluginNode = rootNode.ChildNodes.FirstOrDefault(c => c.SystemName == mainMenuItem.SystemName);
            if(pluginNode != null)
            {
                foreach(var item in listMenus)
                    pluginNode.ChildNodes.Add(item);
            }
            else
            {
                foreach (var item in listMenus)
                    menuItem.ChildNodes.Add(item);
                rootNode.ChildNodes.Add(menuItem);
            }
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task InstallAsync()
        {
            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugins.Widgets.Banner.Configuration"] = "Configuration",
                ["Plugins.Widgets.Banner.BannerConfiguration"] = "Banner - Configuration",
                ["Plugins.Widgets.Banner.BannerConfigure"] = "Configure",
                ["Plugins.Widgets.Banner.BannerList"] = "Banner List",
                ["Plugins.Widgets.Banner.StoreId"] = "Store ID",
                ["Plugins.Widgets.Banner.DisplayOrder"] = "Display Order",
                ["Plugins.Widgets.Banner.Published"] = "Published",

                ["Plugins.Widgets.Banner.Configure.HeaderName"] = "Header Name",
                ["Plugins.Widgets.Banner.Configure.BannerWidth"] = "Banner Width",
                ["Plugins.Widgets.Banner.Configure.BannerHeight"] = "Banner Height",
                ["Plugins.Widgets.Banner.Configure.BannerListPageSize"] = "Banner List Page Size",

                ["Plugins.Widgets.Banner.Configure.Hint.HeaderName"] = "Please Enter the Header Name",
                ["Plugins.Widgets.Banner.Configure.Hint.BannerWidth"] = "Please Enter the Banner Width (in px)",
                ["Plugins.Widgets.Banner.Configure.Hint.BannerHeight"] = "Please Enter the Banner Height (in px)",
                ["Plugins.Widgets.Banner.Configure.Hint.BannerListPageSize"] = "Please Enter the Banner List Page Size",

                ["Plugins.Widgets.Banner.Configure.Required.HeaderName"] = "Header Name is required***",
                ["Plugins.Widgets.Banner.Configure.Required.BannerWidth"] = "Banner Width is required***",
                ["Plugins.Widgets.Banner.Configure.Required.BannerHeight"] = "Banner Height is required***",
                ["Plugins.Widgets.Banner.Configure.Required.BannerListPageSize"] = "Banner List Page Size is required***",

                ["Plugins.Widgets.Banner.Field.Image"] = "Image",
                ["Plugins.Widgets.Banner.Field.BannerName"] = "Banner Name",
                ["Plugins.Widgets.Banner.Field.BannerDescription"] = "Banner Description",
                ["Plugins.Widgets.Banner.Field.Link"] = "Link",
                ["Plugins.Widgets.Banner.Field.LinkName"] = "Link Name",

                ["Plugins.Widgets.Banner.Field.Hint.Image"] = "Please Enter the Image",
                ["Plugins.Widgets.Banner.Field.Hint.BannerName"] = "Please Enter the BannerName",
                ["Plugins.Widgets.Banner.Field.Hint.BannerDescription"] = "Please Enter the BannerDescription",
                ["Plugins.Widgets.Banner.Field.Hint.Link"] = "Please Enter the Link",
                ["Plugins.Widgets.Banner.Field.Hint.LinkName"] = "Please Enter the Link Name",

                ["Plugins.Widgets.Banner.Field.Requierd.Image"] = "Image is required***",
                ["Plugins.Widgets.Banner.Field.Requierd.BannerName"] = "BannerName is required***",
                ["Plugins.Widgets.Banner.Field.Requierd.BannerDescription"] = "BannerDescription is required***",
                ["Plugins.Widgets.Banner.Field.Requierd.Link"] = "Link is required***",
                ["Plugins.Widgets.Banner.Field.Requierd.LinkName"] = "Link Name is required***",


            });

            await base.InstallAsync();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task UninstallAsync()
        {
            _widgetSettings.ActiveWidgetSystemNames.Remove(PluginDefaults.SystemName);
            await _settingService.SaveSettingAsync(_widgetSettings);

            await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.Banner");

            await base.UninstallAsync();
        }
        #endregion
    }
}
