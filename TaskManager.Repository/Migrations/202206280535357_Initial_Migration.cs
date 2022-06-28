namespace TaskManager.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Functions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FunctionName = c.String(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentID, cascadeDelete: true)
                .Index(t => t.DepartmentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Functions", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.Functions", new[] { "DepartmentID" });
            DropTable("dbo.Functions");
            DropTable("dbo.Departments");
        }
    }
}
