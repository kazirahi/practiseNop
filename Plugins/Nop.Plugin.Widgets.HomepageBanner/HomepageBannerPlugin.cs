using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Infrastructure;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.HomepageBanner
{
    public class HomepageBannerPlugin : BasePlugin, IWidgetPlugin
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly INopFileProvider _fileProvider;

        #endregion

        #region CTOR

        public HomepageBannerPlugin(ILocalizationService localizationService,
            IPictureService pictureService,
            ISettingService settingService,
            IWebHelper webHelper,
            INopFileProvider fileProvider)
        {
            _localizationService = localizationService;
            _pictureService = pictureService;
            _settingService = settingService;
            _webHelper = webHelper;
            _fileProvider = fileProvider;
        }

        #endregion

        #region Method
        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <param name="widgetZone"></param>
        /// <returns>return the widget zones</returns>
        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageBeforePoll });
        }

        /// <summary>
        /// get configuration url
        /// </summary>
        /// <returns></returns>
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsHomepageBanner/Configure";
        }

        /// <summary>
        /// gets e name of view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">name of the widget</param>
        /// <returns>view component name</returns>
        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "WidgetsHomepageBanner";
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override async Task InstallAsync()
        {
            //add localization
            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugins.Widgets.Banner.BannerConfiguration"] = "Banner - Configuration",
                ["Plugins.Widgets.Banner.StoreId"] = "Store ID",
                ["Plugins.Widgets.Banner.DisplayOrder"] = "Display Order",
                ["Plugins.Widgets.Banner.Published"] = "Published",
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
        /// uninstall plugin
        /// </summary>
        /// <returns></returns>
        public override async Task UninstallAsync()
        {
            //delete settings
            await _settingService.DeleteSettingAsync<HomepageBannerSettings>();

            //delete localization
            await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.Banner");
            
            await base.UninstallAsync();
        }
        public bool HideInWidgetList => false;

        #endregion
    }
}
