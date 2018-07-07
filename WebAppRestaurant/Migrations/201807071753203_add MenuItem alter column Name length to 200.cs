namespace WebAppRestaurant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMenuItemaltercolumnNamelengthto200 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MenuItems", "Name", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MenuItems", "Name", c => c.String());
        }
    }
}
