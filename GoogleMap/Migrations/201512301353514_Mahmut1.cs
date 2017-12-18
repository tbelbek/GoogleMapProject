namespace GoogleMap.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Mahmut1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartX = c.String(),
                        StartY = c.String(),
                        EndX = c.String(),
                        EndY = c.String(),
                        Distance = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EventStops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventId = c.Int(nullable: false),
                        StopId = c.Int(nullable: false),
                        DistanceBetweenStops = c.Int(),
                        CoordinateX = c.String(),
                        CoordinateY = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Name = c.String(),
                        IconUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Stops");
            DropTable("dbo.EventStops");
            DropTable("dbo.Events");
        }
    }
}
