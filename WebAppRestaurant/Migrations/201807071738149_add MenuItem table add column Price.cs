namespace WebAppRestaurant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMenuItemtableaddcolumnPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MenuItems", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MenuItems", "Price");
        }
    }
}
