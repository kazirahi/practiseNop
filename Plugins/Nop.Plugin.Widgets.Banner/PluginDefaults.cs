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
        public static async Task<IList<string>> AllowedWidgetsZones() 
        {
            var widgetsZones = new List<string>();
            var _fileProvider = EngineContext.Current.Resolve<INopFileProvider>();
            foreach (var filePath in _fileProvider.EnumerateFiles(_fileProvider.MapPath("~/Plugins/Widgets.Banner/Content/"), "*.xml"))
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(filePath);

                var widgetZone = xmlDocument.SelectSingleNode(@"//WidgetZone");

                if (widgetZone == null)
                    continue;

                //load resources
                var values = xmlDocument.SelectNodes("@//WidgetZone/Value");

                if(values == null)
                    continue ;

                foreach (XmlNode xmlNode in values)
                {
                    if (xmlNode.Attributes == null)
                        continue;

                    var widgetName = xmlNode.Attributes["Name"];
                    if(widgetName != null)
                    {
                        widgetsZones.Add(widgetName.Value);
                    }
                }
            }
            return widgetsZones;
        }
    }
}
