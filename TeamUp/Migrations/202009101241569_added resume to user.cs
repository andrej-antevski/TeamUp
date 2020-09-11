namespace TeamUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedresumetouser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Resume_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Resume_Id");
            AddForeignKey("dbo.AspNetUsers", "Resume_Id", "dbo.Resumes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Resume_Id", "dbo.Resumes");
            DropIndex("dbo.AspNetUsers", new[] { "Resume_Id" });
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            DropColumn("dbo.AspNetUsers", "Resume_Id");
        }
    }
}
