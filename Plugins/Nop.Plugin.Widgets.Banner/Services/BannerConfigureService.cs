using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Data;
using Nop.Plugin.Widgets.Banner.Domain;

namespace Nop.Plugin.Widgets.Banner.Services
{
    public partial class BannerConfigureService : IBannerConfigurationService
    {
        #region Constants
        /// <summary>
        /// Cache key for banner configuration
        /// </summary>
        /// <returns></returns>

        private readonly CacheKey _bannerConfigurationAllKey = new("Nop.banner.all-{0}", BANNER_CONFIGURATION_PATTERN_KEY);
        private const string BANNER_CONFIGURATION_PATTERN_KEY = "Nop.banner.";
        #endregion

        #region Fields
        public readonly IRepository<BannerConfigure> _bannerConfigureRepository;
        public readonly IStaticCacheManager _staticCacheManager;
        #endregion

        #region Ctor
        public BannerConfigureService(IRepository<BannerConfigure> bannerConfigureRepository, IStaticCacheManager staticCacheManager)
        {
            _bannerConfigureRepository = bannerConfigureRepository;
            _staticCacheManager = staticCacheManager;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get All Banner Configuration
        /// </summary>
        /// <param name="storeId">store id</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>A task that represents the asynchronous operation
        /// The task result contains the banner configuration</returns>
        public virtual async Task<IPagedList<BannerConfigure>> GetAllBannerConfigurationsAsync(int storeId = 0, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var rez = await _bannerConfigureRepository.GetAllAsync(query =>
            {
                query = query.OrderBy(point => point.HeaderName);

                return query;
            }, cache => cache.PrepareKeyForShortTermCache(_bannerConfigurationAllKey, storeId));

            return new PagedList<Domain.BannerConfigure>(rez, pageIndex, pageSize);
        }

        /// <summary>
        /// Get banner configuration by id
        /// </summary>
        /// <param name="bannerConfigurationId">banner configuration identifier</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<BannerConfigure> GetBannerConfigurationByIdAsync(int bannerConfigurationId)
        {
            return await _bannerConfigureRepository.GetByIdAsync(bannerConfigurationId);
        }

        public virtual async Task InsertBannerConfigurationAsync(BannerConfigure bannerConfigure)
        {
            await _bannerConfigureRepository.InsertAsync(bannerConfigure);
            await _staticCacheManager.RemoveByPrefixAsync(BANNER_CONFIGURATION_PATTERN_KEY);
        }

        public virtual async Task UpdateBannerConfigurationAsync(BannerConfigure bannerConfigure)
        {
            await _bannerConfigureRepository.UpdateAsync(bannerConfigure);
            await _staticCacheManager.RemoveByPrefixAsync(BANNER_CONFIGURATION_PATTERN_KEY);
        }
        public virtual async Task DeleteBannerAsync(BannerConfigure bannerConfigure)
        {
            await _bannerConfigureRepository.DeleteAsync(bannerConfigure);
            await _staticCacheManager.RemoveByPrefixAsync(BANNER_CONFIGURATION_PATTERN_KEY);
        }

        public virtual async Task<IList<Domain.BannerConfigure>> GetAllBannerConfigurationsWithoutPagingAsync()
        {
            var rez = await _bannerConfigureRepository.GetAllAsync(query =>
            {
                query = query.OrderBy(point => point.HeaderName);

                return query;
            }, cache => cache.PrepareKeyForShortTermCache(_bannerConfigurationAllKey));
            return rez;
        }
        #endregion
    }
}
