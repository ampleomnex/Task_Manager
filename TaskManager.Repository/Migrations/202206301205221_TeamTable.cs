namespace TaskManager.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeamTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamName = c.String(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentID, cascadeDelete: true)
                .Index(t => t.DepartmentID);
            
            DropTable("dbo.Epics");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Epics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EPICTitle = c.String(),
                        ProjectName = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Teams", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.Teams", new[] { "DepartmentID" });
            DropTable("dbo.Teams");
        }
    }
}
