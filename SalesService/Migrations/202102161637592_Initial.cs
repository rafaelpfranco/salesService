namespace SalesService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Produtoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.String(),
                        Description = c.String(),
                        RevendedorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Revendedors", t => t.RevendedorId, cascadeDelete: true)
                .Index(t => t.RevendedorId);
            
            CreateTable(
                "dbo.Revendedors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produtoes", "RevendedorId", "dbo.Revendedors");
            DropIndex("dbo.Produtoes", new[] { "RevendedorId" });
            DropTable("dbo.Revendedors");
            DropTable("dbo.Produtoes");
        }
    }
}
