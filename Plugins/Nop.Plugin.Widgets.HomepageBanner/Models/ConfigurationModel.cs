using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.HomepageBanner.Models
{
    public record ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Banner.Field.Image")]
        [UIHint("Picture")]
        public int PictureId { get; set; }
        public bool PictureId_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.Banner.Field.BannerName")]
        public string BannerName { get; set; }
        public bool BannerName_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.Banner.Field.BannerDescription")]
        public string BannerDescription { get; set; }
        public bool BannerDescription_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.Banner.Field.Link")]
        public string Link { get; set; }
        public bool Link_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.Banner.Field.LinkName")]
        public string LinkName { get; set; }
        public bool LinkName_OverrideForStore { get; set; }
    }
}
