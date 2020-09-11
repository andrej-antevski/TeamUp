namespace TeamUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resumemodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        URL = c.String(nullable: false),
                        Resume_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resumes", t => t.Resume_Id)
                .Index(t => t.Resume_Id);
            
            AlterColumn("dbo.Resumes", "Education", c => c.String(nullable: false));
            AlterColumn("dbo.Resumes", "Experience", c => c.String(nullable: false));
            AlterColumn("dbo.Resumes", "HobbiesInterests", c => c.String(nullable: false));
            AlterColumn("dbo.Technologies", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Links", "Resume_Id", "dbo.Resumes");
            DropIndex("dbo.Links", new[] { "Resume_Id" });
            AlterColumn("dbo.Technologies", "Name", c => c.String());
            AlterColumn("dbo.Resumes", "HobbiesInterests", c => c.String());
            AlterColumn("dbo.Resumes", "Experience", c => c.String());
            AlterColumn("dbo.Resumes", "Education", c => c.String());
            DropTable("dbo.Links");
        }
    }
}
