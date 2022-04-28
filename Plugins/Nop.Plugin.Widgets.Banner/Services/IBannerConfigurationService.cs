using Nop.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.Banner.Services
{
    public partial interface IBannerConfigurationService
    {
        /// <summary>
        /// Gets all banner configuration
        /// </summary>
        /// <param name="storeId">The store identifier; pass 0 to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the banner configuration
        /// </returns>
        Task<IPagedList<Domain.BannerConfigure>> GetAllBannerConfigurationsAsync(int storeId = 0, int pageIndex = 0, int pageSize = int.MaxValue);


        /// <summary>
        /// Gets all banner configuration
        /// </summary>
        /// <param name="storeId">The store identifier; pass 0 to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the banner configuration
        /// </returns>
        Task<IList<Domain.BannerConfigure>> GetAllBannerConfigurationsWithoutPagingAsync();

        /// <summary>
        /// Gets a banner configuration
        /// </summary>
        /// <param name="bannerConfigurationId">banner configuration identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the banner Configuration
        /// </returns>
        Task<Domain.BannerConfigure> GetBannerConfigurationByIdAsync(int bannerConfigurationId);

        /// <summary> 
        /// Inserts a banner Configuration
        /// </summary>
        /// <param name="bannerConfigure">banner Configuration</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task InsertBannerConfigurationAsync(Domain.BannerConfigure bannerConfigure);

        /// <summary>
        /// Updates a banner Configuration
        /// </summary>
        /// <param name="bannerConfigure">banner Configuration</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task UpdateBannerConfigurationAsync(Domain.BannerConfigure bannerConfigure);

        /// <summary>
        /// Deletes a banner Configuration
        /// </summary>
        /// <param name="banner">banner Configuration</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeleteBannerAsync(Domain.BannerConfigure bannerConfigure);
    }
}
