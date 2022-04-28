using FluentValidation;
using Nop.Plugin.Widgets.Banner.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Widgets.Banner.Validator
{
    public partial class BannerValidator : BaseNopValidator<BannerModel>
    {
        public BannerValidator(ILocalizationService localizationService)
        {
            //banner name
            RuleFor(model => model.BannerName).NotEmpty().WithMessageAwait(localizationService.GetResourceAsync("Plugins.Widgets.Banner.Field.Requierd.BannerName")).When(c => c.BannerName != null);

            //banner description
            RuleFor(model => model.BannerDescription).NotEmpty().WithMessageAwait(localizationService.GetResourceAsync("Plugins.Widgets.Banner.Field.Requierd.BannerDescription")).When(c => c.BannerDescription != null);

            //banner Link
            RuleFor(model => model.Link).NotEmpty().WithMessageAwait(localizationService.GetResourceAsync("Plugins.Widgets.Banner.Field.Requierd.Link")).When(c => c.Link != null);

            //banner Link name
            RuleFor(model => model.LinkName).NotEmpty().WithMessageAwait(localizationService.GetResourceAsync("Plugins.Widgets.Banner.Field.Requierd.Link")).When(c => c.LinkName != null);
        }
    }
}
