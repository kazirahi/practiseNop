using Nop.Web.Framework.Models;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.Banner.Models
{
    public record BannerPublicInfoModel : BaseNopModel
    {
        public BannerPublicInfoModel()
        {
            BannerModels = new List<BannerModel>();
            BannerConfigureModel = new BannerConfigureModel();

        }
        public IList<BannerModel> BannerModels { get; set; }
        public BannerConfigureModel BannerConfigureModel { get; set; }

    }
}
