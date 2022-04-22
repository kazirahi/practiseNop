using Nop.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.HomepageBanner.Models
{
    public record PublicInfoModel : BaseNopModel
    {
        public string PictureUrl { get; set; }
        public string BannerName { get; set; }
        public string BannerDescription { get; set; }
        public string Link { get; set; }
        public string LinkName { get; set; }
    }
}
