namespace GoogleMap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventHasStartAndEndString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "StartPoint", c => c.String());
            AddColumn("dbo.Events", "FinishPoint", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "FinishPoint");
            DropColumn("dbo.Events", "StartPoint");
        }
    }
}
