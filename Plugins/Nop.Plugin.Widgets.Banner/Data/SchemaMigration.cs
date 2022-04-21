using FluentMigrator;
using Nop.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.Banner.Data
{
    public class SchemaMigration : MigrationBase
    {
        protected IMigrationManager _migrationManager;

        public SchemaMigration(IMigrationManager migrationManager)
        {
            _migrationManager = migrationManager;
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            //
        }
    }
}
