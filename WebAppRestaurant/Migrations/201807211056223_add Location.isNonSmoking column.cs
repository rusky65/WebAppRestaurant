namespace WebAppRestaurant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLocationisNonSmokingcolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "isNonSmoking", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "isNonSmoking");
        }
    }
}
