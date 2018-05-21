namespace SigiApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sigi : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ORIN_ClientCreditNotes", "StateL", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ORIN_ClientCreditNotes", "StateL", c => c.String());
        }
    }
}
