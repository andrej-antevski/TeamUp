namespace TeamUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statustostring : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applications", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applications", "Status");
        }
    }
}
