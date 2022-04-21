using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.Banner.Domain
{
    /// <summary>
    /// Represents a banner configure record
    /// </summary>
    public partial class BannerConfigure : BaseEntity
    {
        public string HeaderName { get; set; }

        /// <summary>
        /// Gets or sets the Banner Description
        /// </summary>
        public int BannerWidth { get; set; }

        /// <summary>
        /// Gets or sets the link
        /// </summary>
        public int BannerHeight { get; set; }

        /// <summary>
        /// Gets or sets the link name
        /// </summary>
        public int BannerListPageSize { get; set; }
    }
}
