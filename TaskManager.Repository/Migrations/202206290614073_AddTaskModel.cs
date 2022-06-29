namespace TaskManager.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaskModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Task_tbl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaskName = c.String(nullable: false),
                        PriorityID = c.Int(nullable: false),
                        EstTime = c.Time(nullable: false, precision: 7),
                        EmployeeID = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Task_tbl");
        }
    }
}
