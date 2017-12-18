namespace GoogleMap.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Mahmut2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Waypoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventId = c.Int(nullable: false),
                        WaypointX = c.Double(nullable: false),
                        WaypointY = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Waypoints");
        }
    }
}
