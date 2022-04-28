using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.Banner.Factories;
using Nop.Plugin.Widgets.Banner.Models;
using Nop.Web.Framework.Components;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.Banner.Component
{
    [ViewComponent(Name = "WidgetsBanner")]
    public class WidgetsBannerViewComponent : NopViewComponent
    {
        #region Fields
        private IBannerModelFactory _bannerModelFactory;
        #endregion

        #region Ctor
        public WidgetsBannerViewComponent(IBannerModelFactory bannerModelFactory)
        {
            _bannerModelFactory = bannerModelFactory;
        }
        #endregion

        #region Methods
        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            //prepare model
            BannerPublicInfoModel model = await _bannerModelFactory.PrepareBannerPublicInfoModelAsync();

            //if model null then return null
            if (model == null)
                return Content("");

            return View("~/Plugins/Widgets.Banner/Views/PublicInfo.cshtml", model);
        }
        #endregion
    }
}
