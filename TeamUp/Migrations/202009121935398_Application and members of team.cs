namespace TeamUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Applicationandmembersofteam : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateSent = c.DateTime(nullable: false),
                        Description = c.String(nullable: false),
                        ForRole_Id = c.Int(),
                        From_Id = c.String(maxLength: 128),
                        To_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RolesNeededs", t => t.ForRole_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.From_Id)
                .ForeignKey("dbo.Teams", t => t.To_Id)
                .Index(t => t.ForRole_Id)
                .Index(t => t.From_Id)
                .Index(t => t.To_Id);
            
            AddColumn("dbo.AspNetUsers", "Team_Id", c => c.Int());
            AddColumn("dbo.Teams", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "Team_Id");
            CreateIndex("dbo.Teams", "ApplicationUser_Id");
            AddForeignKey("dbo.AspNetUsers", "Team_Id", "dbo.Teams", "Id");
            AddForeignKey("dbo.Teams", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applications", "To_Id", "dbo.Teams");
            DropForeignKey("dbo.Applications", "From_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Applications", "ForRole_Id", "dbo.RolesNeededs");
            DropForeignKey("dbo.Teams", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Team_Id", "dbo.Teams");
            DropIndex("dbo.Teams", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Team_Id" });
            DropIndex("dbo.Applications", new[] { "To_Id" });
            DropIndex("dbo.Applications", new[] { "From_Id" });
            DropIndex("dbo.Applications", new[] { "ForRole_Id" });
            DropColumn("dbo.Teams", "ApplicationUser_Id");
            DropColumn("dbo.AspNetUsers", "Team_Id");
            DropTable("dbo.Applications");
        }
    }
}
