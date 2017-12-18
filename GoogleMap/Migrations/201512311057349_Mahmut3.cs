namespace GoogleMap.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Mahmut3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Waypoints", "WaypointX", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Waypoints", "WaypointY", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Waypoints", "WaypointY", c => c.Double(nullable: false));
            AlterColumn("dbo.Waypoints", "WaypointX", c => c.Double(nullable: false));
        }
    }
}
