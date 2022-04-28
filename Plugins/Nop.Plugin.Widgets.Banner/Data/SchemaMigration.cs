using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;

namespace Nop.Plugin.Widgets.Banner.Data
{
    [NopMigration("2022/04/27 09:30:17:6455422", "Banner and Banner configure schema add - 2", MigrationProcessType.Installation)]
    public class SchemaMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.TableFor<Domain.Banner>();
            Create.TableFor<Domain.BannerConfigure>();
        }
    }
}
