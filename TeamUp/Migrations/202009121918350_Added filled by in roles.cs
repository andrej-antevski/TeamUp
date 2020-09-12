namespace TeamUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedfilledbyinroles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RolesNeededs", "Filled", c => c.Boolean(nullable: false));
            AddColumn("dbo.RolesNeededs", "FilledBy_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.RolesNeededs", "FilledBy_Id");
            AddForeignKey("dbo.RolesNeededs", "FilledBy_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RolesNeededs", "FilledBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.RolesNeededs", new[] { "FilledBy_Id" });
            DropColumn("dbo.RolesNeededs", "FilledBy_Id");
            DropColumn("dbo.RolesNeededs", "Filled");
        }
    }
}
