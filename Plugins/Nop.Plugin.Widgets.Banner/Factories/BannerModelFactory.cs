using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.Banner.Infrastructure.Cache;
using Nop.Plugin.Widgets.Banner.Models;
using Nop.Plugin.Widgets.Banner.Services;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Stores;
using Nop.Web.Framework.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.Banner.Factories
{
    public class BannerModelFactory : IBannerModelFactory
    {
        #region Fields

        private readonly IBannerService _bannerService;
        private readonly ILocalizationService _localizationService;
        private readonly IStoreService _storeService;
        private readonly IBannerConfigurationService _configurationService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly ISettingService _settingService;
        private readonly IPictureService _pictureService;
        private readonly IWebHelper _webHelper;
        #endregion

        #region Ctor
        public BannerModelFactory(IBannerService bannerService, ILocalizationService localizationService, IStoreService storeService, IBannerConfigurationService bannerConfigurationService, IStaticCacheManager staticCacheManager, ISettingService settingService, IPictureService pictureService, IWebHelper webHelper)
        {
            _bannerService = bannerService;
            _localizationService = localizationService;
            _storeService = storeService;
            _configurationService = bannerConfigurationService;
            _settingService = settingService;
            _pictureService = pictureService;
            _webHelper = webHelper;
            _staticCacheManager = staticCacheManager;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Prepare store banner list model
        /// </summary>
        /// <param name="searchModel">Store pickup point search model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the store banner list model
        /// </returns>
        public async Task<BannerListModel> PrepareBannerListModelAsync(BannerSearchModel searchModel)
        {
            var getConfiguration = _configurationService.GetAllBannerConfigurationsWithoutPagingAsync().Result;
            var banner = await _bannerService.GetAllBannersAsync(pageIndex: searchModel.Page - 1,
                pageSize: getConfiguration.Count > 0 ? getConfiguration[0].BannerListPageSize : searchModel.PageSize);
            var model = await new BannerListModel().PrepareToGridAsync(searchModel, banner, () =>
            {
                return banner.SelectAwait(async point =>
                {
                    var store = await _storeService.GetStoreByIdAsync(point.StoreId);

                    return new BannerModel
                    {
                        Id = point.Id,
                        BannerName = point.BannerName,
                        BannerDescription = point.BannerDescription,
                        Link = point.Link,
                        LinkName = point.LinkName,
                        DisplayOrder = point.DisplayOrder,
                        Published = point.Published,
                        PictureId = point.PictureId,
                        PictureUrl = await GetPictureUrlAsync(point.PictureId),
                        StoreName = store?.Name
                            ?? (point.StoreId == 0 ? (await _localizationService.GetResourceAsync("Admin.Configuration.Settings.StoreScope.AllStores")) : string.Empty)
                    };
                });
            });

            return model;
        }

        /// <summary>
        /// Prepare model for public info
        /// </summary>
        /// <returns></returns>
        public async Task<BannerPublicInfoModel> PrepareBannerPublicInfoModelAsync()
        {
            var model = new BannerPublicInfoModel();

            var getConfigure = _configurationService.GetAllBannerConfigurationsAsync().Result.FirstOrDefault();
            if (getConfigure == null)
                return null;

            var getBanner = _bannerService.GetAllBannersAsync().Result.Where(x => x.Published == true).OrderBy(x => x.DisplayOrder);

            if (!getBanner.Any())
                return null;

            BannerModel bannerModel = new BannerModel();
            List<BannerModel> bannerModels = new List<BannerModel>();
            foreach (var modelItem in getBanner)
            {
                bannerModel = new BannerModel()
                {
                    BannerName = modelItem.BannerName,
                    BannerDescription = modelItem.BannerDescription,
                    Link = modelItem.Link,
                    LinkName = modelItem.LinkName,
                    DisplayOrder = modelItem.DisplayOrder,
                    Published = modelItem.Published,
                    PictureId = modelItem.PictureId,
                    PictureUrl = await GetPictureUrlAsync(modelItem.PictureId)

                };
                bannerModels.Add(bannerModel);
            }

            model.BannerConfigureModel.HeaderName = getConfigure.HeaderName;
            model.BannerConfigureModel.BannerHeight = getConfigure.BannerHeight;
            model.BannerConfigureModel.BannerWidth = getConfigure.BannerWidth;
            model.BannerConfigureModel.BannerInRow = getConfigure.BannerInRow;
            model.BannerConfigureModel.BannerListPageSize = getConfigure.BannerListPageSize;

            model.BannerModels = bannerModels;

            return model;

        }

        /// <summary>
        /// Prepare store banner search model
        /// </summary>
        /// <param name="searchModel">Store banner search model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the store banner search model
        /// </returns>
        public Task<BannerSearchModel> PrepareBannerSearchModelAsync(BannerSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //prepare page parameters
            searchModel.SetGridPageSize();

            return Task.FromResult(searchModel);
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
