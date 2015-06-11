namespace TurboRango.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Avaliacao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Avaliacaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Data = c.DateTime(nullable: false),
                        Restaurante_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurantes", t => t.Restaurante_Id)
                .Index(t => t.Restaurante_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Avaliacaos", "Restaurante_Id", "dbo.Restaurantes");
            DropIndex("dbo.Avaliacaos", new[] { "Restaurante_Id" });
            DropTable("dbo.Avaliacaos");
        }
    }
}
