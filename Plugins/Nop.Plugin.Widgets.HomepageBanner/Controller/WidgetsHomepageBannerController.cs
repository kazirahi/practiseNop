using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.HomepageBanner.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.HomepageBanner.Controller
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class WidgetsHomepageBannerController : BasePluginController
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;

        #endregion

        #region Ctor
        public WidgetsHomepageBannerController(ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService,
            IPictureService pictureService,
            ISettingService settingService,
            IStoreContext storeContext)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _pictureService = pictureService;
            _settingService = settingService;
            _storeContext = storeContext;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Configuration from admin panel
        /// </summary>
        /// <returns>model return for view page</returns>
        public async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load setting for a chosen store scope
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var homepageBannerSettings = await _settingService.LoadSettingAsync<HomepageBannerSettings>(storeScope);

            var model = new ConfigurationModel
            {
                PictureId = homepageBannerSettings.PictureId,
                BannerName = homepageBannerSettings.BannerName,
                BannerDescription = homepageBannerSettings.BannerDescription,
                Link = homepageBannerSettings.Link,
                LinkName = homepageBannerSettings.LinkName,
            };
            if (storeScope > 0)
            {
                model.PictureId_OverrideForStore = await _settingService.SettingExistsAsync(homepageBannerSettings, x => x.PictureId, storeScope);
                model.BannerName_OverrideForStore = await _settingService.SettingExistsAsync(homepageBannerSettings, x => x.BannerName, storeScope);
                model.BannerDescription_OverrideForStore = await _settingService.SettingExistsAsync(homepageBannerSettings, x => x.BannerDescription, storeScope);
                model.Link_OverrideForStore = await _settingService.SettingExistsAsync(homepageBannerSettings, x => x.Link, storeScope);
                model.LinkName_OverrideForStore = await _settingService.SettingExistsAsync(homepageBannerSettings, x => x.LinkName, storeScope);
            }

            return View("~/Plugins/Widgets.HomepageBanner/Views/Configure.cshtml", model);
        }
        [HttpPost]
        public async Task<IActionResult> Configure(ConfigurationModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load setting for a chosen store scope
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var homepageBannerSettings = await _settingService.LoadSettingAsync<HomepageBannerSettings>(storeScope);

            //get previous pucture identifiers
            var previousPictureIds = homepageBannerSettings.PictureId;

            homepageBannerSettings.PictureId = model.PictureId;
            homepageBannerSettings.BannerName = model.BannerName;
            homepageBannerSettings.BannerDescription = model.BannerDescription;
            homepageBannerSettings.LinkName = model.LinkName;
            homepageBannerSettings.Link = model.Link;

            await _settingService.SaveSettingOverridablePerStoreAsync(homepageBannerSettings, x => x.PictureId, model.PictureId_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(homepageBannerSettings, x => x.BannerName, model.BannerName_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(homepageBannerSettings, x => x.BannerDescription, model.BannerDescription_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(homepageBannerSettings, x => x.Link, model.Link_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(homepageBannerSettings, x => x.LinkName, model.LinkName_OverrideForStore, storeScope, false);

            //clear settings cache
            await _settingService.ClearCacheAsync();

            //get current pucture identifiers 
            var currentPictureIds = homepageBannerSettings.PictureId;

            //delete old picture
            var getPicuter = await _pictureService.GetPictureByIdAsync(previousPictureIds);
            if (getPicuter != null)
                await _pictureService.DeletePictureAsync(getPicuter);

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

            return View("~/Plugins/Widgets.HomepageBanner/Views/Configure.cshtml", model);
        }
        #endregion
    }
}
