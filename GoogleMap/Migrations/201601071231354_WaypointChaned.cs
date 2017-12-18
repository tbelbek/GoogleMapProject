namespace GoogleMap.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class WaypointChaned : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Waypoints", "WaypointX", c => c.String());
            AlterColumn("dbo.Waypoints", "WaypointY", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Waypoints", "WaypointY", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Waypoints", "WaypointX", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
