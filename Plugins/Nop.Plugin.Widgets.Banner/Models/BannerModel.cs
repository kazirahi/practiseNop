using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Widgets.Banner.Models
{
    public record BannerModel : BaseNopEntityModel
    {
        public BannerModel()
        {
            AvailableStores = new List<SelectListItem>();
        }
        public List<SelectListItem> AvailableStores { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.Banner.Field.StoreId")]
        public int StoreId { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.Banner.Field.Image")]
        [UIHint("Picture")]
        public int PictureId { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.Banner.Field.BannerName")]
        public string BannerName { get; set; }
        public string StoreName { get; set; }
        public string PictureUrl { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.Banner.Field.BannerDescription")]
        public string BannerDescription { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.Banner.Field.Link")]
        public string Link { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.Banner.Field.LinkName")]
        public string LinkName { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.Banner.DisplayOrder")]
        public int DisplayOrder { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.Banner.Published")]
        public bool Published { get; set; }
    }
}
