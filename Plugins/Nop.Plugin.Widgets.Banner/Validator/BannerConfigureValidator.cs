using FluentValidation;
using Nop.Plugin.Widgets.Banner.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Widgets.Banner.Validator
{
    public partial class BannerConfigureValidator : BaseNopValidator<BannerConfigureModel>
    {
        public BannerConfigureValidator(ILocalizationService localizationService)
        {
            //header name
            RuleFor(model => model.HeaderName).NotEmpty().WithMessageAwait(localizationService.GetResourceAsync("Plugins.Widgets.Banner.Configure.Required.HeaderName")).When(c => c.HeaderName != null);

            //banner width
            RuleFor(model => model.BannerWidth)
                .InclusiveBetween(0, 1200)
                .NotEmpty().WithMessageAwait(localizationService.GetResourceAsync("Plugins.Widgets.Banner.Configure.Required.BannerWidth"));

            //banner height
            RuleFor(model => model.BannerHeight).NotEmpty().WithMessageAwait(localizationService.GetResourceAsync("Plugins.Widgets.Banner.Configure.Required.BannerHeight"));

            //banner list per page
            RuleFor(model => model.BannerListPageSize).NotEmpty().WithMessageAwait(localizationService.GetResourceAsync("Plugins.Widgets.Banner.Configure.Required.BannerListPageSize"));

            //banner in row
            RuleFor(model => model.BannerInRow)
                .InclusiveBetween(1, 4)
                .NotEmpty().WithMessageAwait(localizationService.GetResourceAsync("Plugins.Widgets.Banner.Configure.Required.BannerInRow"));
        }
    }
}
