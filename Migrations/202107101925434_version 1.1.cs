namespace NewsApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.AspNetUsers", "AvatarUrl", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "AvatarUrl");
            DropColumn("dbo.AspNetUsers", "Name");
        }
    }
}
