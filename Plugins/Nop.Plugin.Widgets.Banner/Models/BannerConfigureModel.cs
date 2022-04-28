using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;


namespace Nop.Plugin.Widgets.Banner.Models
{
    public record BannerConfigureModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Plugins.Widgets.Banner.Configure.HeaderName")]
        public string HeaderName { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.Banner.Configure.BannerWidth")]
        public int BannerWidth { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.Banner.Configure.BannerHeight")]
        public int BannerHeight { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.Banner.Configure.BannerListPageSize")]
        public int BannerListPageSize { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.Banner.Configure.BannerInRow")]
        public int BannerInRow { get; set; }
    }
}
