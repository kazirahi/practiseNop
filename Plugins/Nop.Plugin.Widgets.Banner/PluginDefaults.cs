using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Web.Framework.Infrastructure;


namespace Nop.Plugin.Widgets.Banner
{
    public static class PluginDefaults
    {
        public static string BannerFilters => "widgets_banner";
        public static string SystemName => "Widgets.Banner";
        public static string ConfigurationRouteName => "Plugins.Widgets.Banner.Configuration";
        public static string ListRouteName => "Plugins.Widgets.Banner.BannerList";

    }
}
