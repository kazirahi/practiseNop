using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.Banner.Factories;
using Nop.Plugin.Widgets.Banner.Infrastructure.Cache;
using Nop.Plugin.Widgets.Banner.Models;
using Nop.Plugin.Widgets.Banner.Services;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.Banner.Controllers
{
    [Area(AreaNames.Admin)]
    [AuthorizeAdmin]
    [AutoValidateAntiforgeryToken]
    public class WidgetsBannerController : BasePluginController
    {
        #region Fields

        private readonly IBannerService _bannerService;
        private readonly IBannerConfigurationService _bannerConfigurationService;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly INotificationService _notificationService;
        private readonly IBannerModelFactory _bannerModelFactory;
        private readonly IStoreService _storeService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IWebHelper _webHelper;
        private readonly IPictureService _pictureService;

        #endregion

        #region Ctor
        public WidgetsBannerController(IBannerService bannerService, IBannerConfigurationService bannerConfigurationService, ILocalizationService localizationService, INotificationService notificationService, IPermissionService permissionService, IBannerModelFactory bannerModelFactory, IStoreService storeService,
            IStaticCacheManager staticCacheManager,
            IWebHelper webHelper, IPictureService pictureService)
        {
            _bannerService = bannerService;
            _bannerConfigurationService = bannerConfigurationService;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _bannerModelFactory = bannerModelFactory;
            _storeService = storeService;
            _staticCacheManager = staticCacheManager;
            _webHelper = webHelper;
            _pictureService = pictureService;
        }
        #endregion

        #region Methods

        public async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var getData = _bannerConfigurationService.GetAllBannerConfigurationsWithoutPagingAsync().Result.FirstOrDefault();
            var model = new BannerConfigureModel();
            if (getData != null)
            {
                model = new BannerConfigureModel()
                {
                    HeaderName = getData.HeaderName,
                    BannerHeight = getData.BannerHeight,
                    BannerWidth = getData.BannerWidth,
                    BannerListPageSize = getData.BannerListPageSize,
                    BannerInRow = getData.BannerInRow,
                    Id = getData.Id,
                };
            }
            return View("~/Plugins/Widgets.Banner/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Configure(BannerConfigureModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var banner = new Domain.BannerConfigure()
                {
                    BannerHeight = model.BannerHeight,
                    BannerWidth = model.BannerWidth,
                    BannerInRow = model.BannerInRow,
                    BannerListPageSize = model.BannerListPageSize,
                    HeaderName = model.HeaderName,
                    Id = model.Id,
                };
                if (model.Id > 0)
                {
                    await _bannerConfigurationService.UpdateBannerConfigurationAsync(banner);
                    _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

                }
                else
                {
                    await _bannerConfigurationService.InsertBannerConfigurationAsync(banner);
                    _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));
                }
            }

            return View("~/Plugins/Widgets.Banner/Views/Configure.cshtml", model);
        }

        //list page
        public async Task<IActionResult> List()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //prepare model
            var model = await _bannerModelFactory.PrepareBannerSearchModelAsync(new BannerSearchModel());

            return View("~/Plugins/Widgets.Banner/Views/List.cshtml", model);
        }

        //render data from list page
        [HttpPost]
        public async Task<IActionResult> GetAllData(BannerSearchModel searchModel)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageShippingSettings))
                return await AccessDeniedDataTablesJson();

            //prepare model
            var model = await _bannerModelFactory.PrepareBannerListModelAsync(searchModel);

            return Json(model);
        }

        public async Task<IActionResult> Create()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageShippingSettings))
                return AccessDeniedView();

            //check configure settings or not
            //if configure empty then return the configure page
            var getCongigure = _bannerConfigurationService.GetAllBannerConfigurationsAsync().Result.FirstOrDefault();
            if (getCongigure == null)
                return RedirectToAction("Configure");

            var model = new BannerModel();

            //store store
            model.AvailableStores.Add(new SelectListItem { Text = await _localizationService.GetResourceAsync("Admin.Configuration.Settings.StoreScope.AllStores"), Value = "0" });
            foreach (var store in await _storeService.GetAllStoresAsync())
                model.AvailableStores.Add(new SelectListItem { Text = store.Name, Value = store.Id.ToString() });
            return View("~/Plugins/Widgets.Banner/Views/Create.cshtml", model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BannerModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageShippingSettings))
                return AccessDeniedView();

            //check configure settings or not
            //if configure empty then return the configure page
            var getCongigure = _bannerConfigurationService.GetAllBannerConfigurationsAsync().Result.FirstOrDefault();
            if (getCongigure == null)
                return RedirectToAction("Configure");

            if (ModelState.IsValid)
            {
                var banner = new Domain.Banner()
                {
                    BannerName = model.BannerName,
                    BannerDescription = model.BannerDescription,
                    PictureId = model.PictureId,
                    Link = model.Link,
                    LinkName = model.LinkName,
                    Published = model.Published,
                    DisplayOrder = model.DisplayOrder,
                    StoreId = model.StoreId,
                };
                await _bannerService.InsertBannerAsync(banner);
                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));
                ViewBag.RefreshPage = true;
            }
            return View("~/Plugins/Widgets.Banner/Views/Create.cshtml", model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageShippingSettings))
                return AccessDeniedView();

            //check configure settings or not
            //if configure empty then return the configure page
            var getCongigure = _bannerConfigurationService.GetAllBannerConfigurationsAsync().Result.FirstOrDefault();
            if (getCongigure == null)
                return RedirectToAction("Configure");

            var getBanner = _bannerService.GetBannerByIdAsync(id).Result;
            var model = new BannerModel()
            {
                Id = getBanner.Id,
                BannerName = getBanner.BannerName,
                BannerDescription = getBanner.BannerDescription,
                Link = getBanner.Link,
                LinkName = getBanner.LinkName,
                StoreId = getBanner.StoreId,
                PictureId = getBanner.PictureId,
                PictureUrl = await GetPictureUrlAsync(getBanner.Id),
                DisplayOrder = getBanner.DisplayOrder,
                Published = getBanner.Published,

            };
            model.AvailableStores.Add(new SelectListItem { Text = await _localizationService.GetResourceAsync("Admin.Configuration.Settings.StoreScope.AllStores"), Value = "0" });
            foreach (var store in await _storeService.GetAllStoresAsync())
                model.AvailableStores.Add(new SelectListItem { Text = store.Name, Value = store.Id.ToString(), Selected = store.Id == model.StoreId });

            return View("~/Plugins/Widgets.Banner/Views/Edit.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BannerModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageShippingSettings))
                return AccessDeniedView();

            //check configure settings or not
            //if configure empty then return the configure page
            var getCongigure = _bannerConfigurationService.GetAllBannerConfigurationsAsync().Result.FirstOrDefault();
            if (getCongigure == null)
                return RedirectToAction("Configure");

            if (!ModelState.IsValid)
                return await Edit(model.Id);

            var getBanner = _bannerService.GetBannerByIdAsync(model.Id).Result;
            if (getBanner == null)
                return RedirectToAction("List");

            var banner = new Domain.Banner()
            {
                Id = model.Id,
                BannerName = model.BannerName,
                BannerDescription = model.BannerDescription,
                Link = model.Link,
                LinkName = model.LinkName,
                StoreId = model.StoreId,
                PictureId = model.PictureId,
                DisplayOrder = model.DisplayOrder,
                Published = model.Published,

            };

            await _bannerService.UpdateBannerAsync(banner);

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

            ViewBag.RefreshPage = true;
            return View("~/Plugins/Widgets.Banner/Views/Edit.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageShippingSettings))
                return AccessDeniedView();

            var getBanner = await _bannerService.GetBannerByIdAsync(id);
            if (getBanner == null)
                return RedirectToAction("List");

            //get picture for delete
            var getPicture = await _pictureService.GetPictureByIdAsync(getBanner.PictureId);
            await _bannerService.DeleteBannerAsync(getBanner);

            if (getBanner != null)
                await _pictureService.DeletePictureAsync(getPicture);


            return new NullJsonResult();
        }
        //get picture url
        protected async Task<string> GetPictureUrlAsync(int pictureId)
        {
            var cachekey = _staticCacheManager.PrepareKeyForDefaultCache(ModelCacheEventConsumer.PICTURE_URL_MODEL_KEY,
                pictureId, _webHelper.IsCurrentConnectionSecured() ? Uri.UriSchemeHttps : Uri.UriSchemeHttp);

            return await _staticCacheManager.GetAsync(cachekey, async () =>
            {
                var url = await _pictureService.GetPictureUrlAsync(pictureId, showDefaultPicture: false) ?? "";
                return url;
            });
        }
        #endregion
    }
}
