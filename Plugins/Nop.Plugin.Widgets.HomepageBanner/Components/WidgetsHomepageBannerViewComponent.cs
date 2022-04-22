using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.HomepageBanner.Infrastructure.Cache;
using Nop.Plugin.Widgets.HomepageBanner.Models;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.HomepageBanner.Components
{
    [ViewComponent(Name = "WidgetsHomepageBanner")]
    public class WidgetsHomepageBannerViewComponent : NopViewComponent
    {
        #region Fields

        private readonly IStoreContext _storeContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly ISettingService _settingService;
        private readonly IPictureService _pictureService;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        public WidgetsHomepageBannerViewComponent(IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,
            ISettingService settingService,
            IPictureService pictureService,
            IWebHelper webHelper)
        {
            _storeContext = storeContext;
            _staticCacheManager = staticCacheManager;
            _settingService = settingService;
            _pictureService = pictureService;
            _webHelper = webHelper;
        }

        #endregion

        #region Methods
        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            //load current store
            var store = await _storeContext.GetCurrentStoreAsync();
            var homepageBannerSettings = await _settingService.LoadSettingAsync<HomepageBannerSettings>(store.Id);

            //prepare model
            var model = new PublicInfoModel
            {
                PictureUrl = await GetPictureUrlAsync(homepageBannerSettings.PictureId),
                BannerName = homepageBannerSettings.BannerName,
                BannerDescription = homepageBannerSettings.BannerDescription,
                Link = homepageBannerSettings.Link,
                LinkName = homepageBannerSettings.LinkName
            };

            if (string.IsNullOrEmpty(model.PictureUrl))
                //no pictures uploaded
                return Content("");

            return View("~/Plugins/Widgets.HomepageBanner/Views/PublicInfo.cshtml", model);
        }

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
