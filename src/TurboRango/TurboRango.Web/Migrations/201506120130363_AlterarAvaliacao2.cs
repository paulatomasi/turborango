namespace TurboRango.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterarAvaliacao2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Avaliacaos", "Nota", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Avaliacaos", "Nota");
        }
    }
}
