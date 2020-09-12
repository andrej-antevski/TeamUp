namespace TeamUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allowduedatenull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Teams", "DueDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Teams", "DueDate", c => c.DateTime(nullable: false));
        }
    }
}
