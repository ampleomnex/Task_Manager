namespace TaskManager.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeIDDataTypeChangedInTaskTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Task_tbl", "EmployeeID", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Task_tbl", "EmployeeID", c => c.Int(nullable: false));
        }
    }
}
