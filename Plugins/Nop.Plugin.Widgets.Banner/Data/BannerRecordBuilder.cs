using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.Banner.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.Banner.Data
{
    public partial class BannerRecordBuilder : NopEntityBuilder<Domain.Banner>
    {
        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(Domain.Banner.BannerName)).AsString().NotNullable()
                .WithColumn(nameof(Domain.Banner.BannerDescription)).AsString().NotNullable()
                .WithColumn(nameof(Domain.Banner.Link)).AsString().NotNullable()
                .WithColumn(nameof(Domain.Banner.LinkName)).AsString().NotNullable()
                .WithColumn(nameof(Domain.Banner.PictureId)).AsInt32().NotNullable()
                .WithColumn(nameof(Domain.Banner.StoreId)).AsInt32().NotNullable()
                .WithColumn(nameof(Domain.Banner.DisplayOrder)).AsInt32().NotNullable()
                .WithColumn(nameof(Domain.Banner.DisplayOrder)).AsInt32().NotNullable()
                .WithColumn(nameof(Domain.Banner.Published)).AsBoolean().NotNullable();
        }
    }
}
