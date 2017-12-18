namespace GoogleMap.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class name : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "ImageSrc", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "ImageSrc");
        }
    }
}
