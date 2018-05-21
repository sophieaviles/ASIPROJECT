namespace SigiApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fapa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ODPI_DownPayment", "BaseDocEntry", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ODPI_DownPayment", "BaseDocEntry");
        }
    }
}
