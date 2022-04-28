using Nop.Core;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.Banner.Services
{
    public partial interface IBannerService
    {
        /// <summary>
        /// Gets all banner
        /// </summary>
        /// <param name="storeId">The store identifier; pass 0 to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the banners
        /// </returns>
        Task<IPagedList<Domain.Banner>> GetAllBannersAsync(int storeId = 0, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Gets a banner
        /// </summary>
        /// <param name="bannerId">banner identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the banner
        /// </returns>
        Task<Domain.Banner> GetBannerByIdAsync(int bannerId);

        /// <summary>
        /// Inserts a banner
        /// </summary>
        /// <param name="banner">banner</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task InsertBannerAsync(Domain.Banner banner);

        /// <summary>
        /// Updates a banner
        /// </summary>
        /// <param name="banner">banner</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task UpdateBannerAsync(Domain.Banner banner);

        /// <summary>
        /// Deletes a banner
        /// </summary>
        /// <param name="banner">banner</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeleteBannerAsync(Domain.Banner banner);
    }
}
