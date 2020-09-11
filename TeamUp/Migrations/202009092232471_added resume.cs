namespace TeamUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedresume : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Resumes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Education = c.String(),
                        Experience = c.String(),
                        HobbiesInterests = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Resumes");
        }
    }
}
