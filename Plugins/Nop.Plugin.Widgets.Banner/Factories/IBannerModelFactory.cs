using Nop.Plugin.Widgets.Banner.Models;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.Banner.Factories
{
    public interface IBannerModelFactory
    {
        /// <summary>
        /// Prepare store banner list model
        /// </summary>
        /// <param name="searchModel">Store pickup point search model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the store banner list model
        /// </returns>
        Task<BannerListModel> PrepareBannerListModelAsync(BannerSearchModel searchModel);

        /// <summary>
        /// Prepare store banner search model
        /// </summary>
        /// <param name="searchModel">Store pickup point search model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the store banner search model
        /// </returns>
        Task<BannerSearchModel> PrepareBannerSearchModelAsync(BannerSearchModel searchModel);

        /// <summary>
        /// Prepare store banner complete model for public info
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// </returns>
        Task<BannerPublicInfoModel> PrepareBannerPublicInfoModelAsync();
    }
}
