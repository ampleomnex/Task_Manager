namespace TaskManager.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedstatuscolumninDepartment : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Departments", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Departments", "Status", c => c.Int(nullable: false));
        }
    }
}
