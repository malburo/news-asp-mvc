namespace NewsApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version12 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Post", "Content", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Post", "Content", c => c.String(maxLength: 2000));
        }
    }
}
