namespace TeamUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmessage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        TimeSent = c.DateTime(nullable: false),
                        From_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.From_Id)
                .Index(t => t.From_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "From_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "From_Id" });
            DropTable("dbo.Messages");
        }
    }
}
