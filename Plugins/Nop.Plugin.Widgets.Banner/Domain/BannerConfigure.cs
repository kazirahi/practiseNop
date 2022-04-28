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
        /// <summary>
        /// Gets or sets the Header Name
        /// </summary>
        public string HeaderName { get; set; }

        /// <summary>
        /// Gets or sets the Banner Width
        /// </summary>
        public int BannerWidth { get; set; }

        /// <summary>
        /// Gets or sets the Banner Height
        /// </summary>
        public int BannerHeight { get; set; }

        /// <summary>
        /// Gets or sets the Banner list page size
        /// </summary>
        public int BannerListPageSize { get; set; }

        /// <summary>
        /// Gets or sets how many in row
        /// </summary>
        public int BannerInRow { get; set; }
    }
}
