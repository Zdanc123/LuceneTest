namespace LuceneTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CharacterEpisodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CharacterID = c.Int(nullable: false),
                        EpisodeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Characters", t => t.CharacterID, cascadeDelete: true)
                .ForeignKey("dbo.Episodes", t => t.EpisodeID, cascadeDelete: true)
                .Index(t => t.CharacterID)
                .Index(t => t.EpisodeID);
            
            CreateTable(
                "dbo.Episodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CharacterEpisodes", "EpisodeID", "dbo.Episodes");
            DropForeignKey("dbo.CharacterEpisodes", "CharacterID", "dbo.Characters");
            DropIndex("dbo.CharacterEpisodes", new[] { "EpisodeID" });
            DropIndex("dbo.CharacterEpisodes", new[] { "CharacterID" });
            DropTable("dbo.Episodes");
            DropTable("dbo.CharacterEpisodes");
            DropTable("dbo.Characters");
        }
    }
}
