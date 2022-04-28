using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;


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
                .WithColumn(nameof(Domain.Banner.BannerName)).AsString(int.MaxValue).NotNullable()
                .WithColumn(nameof(Domain.Banner.BannerDescription)).AsString(int.MaxValue).NotNullable()
                .WithColumn(nameof(Domain.Banner.Link)).AsString(int.MaxValue).NotNullable()
                .WithColumn(nameof(Domain.Banner.LinkName)).AsString(int.MaxValue).NotNullable()
                .WithColumn(nameof(Domain.Banner.PictureId)).AsInt32().NotNullable()
                .WithColumn(nameof(Domain.Banner.StoreId)).AsInt32().NotNullable()
                .WithColumn(nameof(Domain.Banner.DisplayOrder)).AsInt32().NotNullable()
                .WithColumn(nameof(Domain.Banner.Published)).AsBoolean().NotNullable();
        }
    }
}
