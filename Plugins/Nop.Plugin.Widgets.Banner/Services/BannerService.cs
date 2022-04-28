using System;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Data;


namespace Nop.Plugin.Widgets.Banner.Services
{
    public partial class BannerService : IBannerService
    {
        #region Constants
        /// <summary>
        /// Cache key for banner
        /// </summary>
        /// <remarks>
        /// {0} : current store ID
        /// </remarks>
        private readonly CacheKey _bannerAllKey = new("Nop.banner.all-{0}", BANNER_PATTERN_KEY);
        private const string BANNER_PATTERN_KEY = "Nop.banner.";
        #endregion

        #region Fields
        public readonly IRepository<Domain.Banner> _bannerRepository;
        public readonly IStaticCacheManager _cacheManager;
        #endregion

        #region Ctor
        public BannerService(IRepository<Domain.Banner> bannerRepository, IStaticCacheManager staticCacheManager)
        {
            _bannerRepository = bannerRepository;
            _cacheManager = staticCacheManager;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Gets all banners
        /// </summary>
        /// <param name="storeId">The store identifier; pass 0 to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the banner
        /// </returns>
        public virtual async Task<IPagedList<Domain.Banner>> GetAllBannersAsync(int storeId = 0, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var rez = await _bannerRepository.GetAllAsync(query =>
            {
                if (storeId > 0)
                    query = query.Where(point => point.StoreId == storeId || point.StoreId == 0);
                query = query.OrderBy(point => point.DisplayOrder).ThenBy(point => point.BannerName);

                return query;
            }, cache => cache.PrepareKeyForShortTermCache(_bannerAllKey, storeId));

            return new PagedList<Domain.Banner>(rez, pageIndex, pageSize);
        }

        /// <summary>
        /// Get a Banner by Banner id
        /// </summary>
        /// <param name="bannerId">banner identifier</param>
        /// <returns>return the banner by id</returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<Domain.Banner> GetBannerByIdAsync(int bannerId)
        {
            return await _bannerRepository.GetByIdAsync(bannerId);
        }

        /// <summary>
        /// Insert the banner info
        /// </summary>
        /// <param name="banner">banner model</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task InsertBannerAsync(Domain.Banner banner)
        {
            await _bannerRepository.InsertAsync(banner);
            await _cacheManager.RemoveByPrefixAsync(BANNER_PATTERN_KEY);
        }

        /// <summary>
        /// Update banner
        /// </summary>
        /// <param name="banner">banner model</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task UpdateBannerAsync(Domain.Banner banner)
        {
            await _bannerRepository.UpdateAsync(banner);
            await _cacheManager.RemoveByPrefixAsync(BANNER_PATTERN_KEY);
        }

        /// <summary>
        /// Delete banner
        /// </summary>
        /// <param name="banner">banner model</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteBannerAsync(Domain.Banner banner)
        {
            await _bannerRepository.DeleteAsync(banner, false);
            await _cacheManager.RemoveByPrefixAsync(BANNER_PATTERN_KEY);
        }
        #endregion
    }
}
