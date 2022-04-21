using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.Banner.Data
{
    public partial class BanngerCofigureRecordBuilder : NopEntityBuilder<Domain.BannerConfigure>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(Domain.BannerConfigure.HeaderName)).AsString().NotNullable()
                .WithColumn(nameof(Domain.BannerConfigure.BannerWidth)).AsInt32().NotNullable()
                .WithColumn(nameof(Domain.BannerConfigure.BannerHeight)).AsInt32().NotNullable()
                .WithColumn(nameof(Domain.BannerConfigure.BannerListPageSize)).AsInt32().NotNullable();
        }
    }
}
