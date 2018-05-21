namespace SigiApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambios : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OINM_Transaction", "DocDate", c => c.DateTime());
            AlterColumn("dbo.OINM_Transaction", "DocDueDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OINM_Transaction", "DocDueDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OINM_Transaction", "DocDate", c => c.DateTime(nullable: false));
        }
    }
}
