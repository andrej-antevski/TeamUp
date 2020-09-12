namespace TeamUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedteams : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RolesNeededs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Team_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.Team_Id)
                .Index(t => t.Team_Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        Admin_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Admin_Id)
                .Index(t => t.Admin_Id);
            
            AddColumn("dbo.Messages", "Team_Id", c => c.Int());
            CreateIndex("dbo.Messages", "Team_Id");
            AddForeignKey("dbo.Messages", "Team_Id", "dbo.Teams", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RolesNeededs", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.Messages", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.Teams", "Admin_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Teams", new[] { "Admin_Id" });
            DropIndex("dbo.RolesNeededs", new[] { "Team_Id" });
            DropIndex("dbo.Messages", new[] { "Team_Id" });
            DropColumn("dbo.Messages", "Team_Id");
            DropTable("dbo.Teams");
            DropTable("dbo.RolesNeededs");
        }
    }
}
