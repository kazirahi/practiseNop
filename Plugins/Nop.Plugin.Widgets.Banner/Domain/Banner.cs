using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.Banner.Domain
{
    /// <summary>
    /// represents a banner record
    /// </summary>
    public partial class Banner : BaseEntity
    {
        /// <summary>
        /// Gets or sets the store identifier
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Gets or sets the picture identifier
        /// </summary>
        public int PictureId { get; set; }

        /// <summary>
        /// Gets or sets the Banner Name
        /// </summary>
        public string BannerName { get; set; }

        /// <summary>
        /// Gets or sets the Banner Description
        /// </summary>
        public string BannerDescription { get; set; }

        /// <summary>
        /// Gets or sets the link
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the link name
        /// </summary>
        public string LinkName { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the published
        /// </summary>
        public bool Published { get; set; }

    }
}
