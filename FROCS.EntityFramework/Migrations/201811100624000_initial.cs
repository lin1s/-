namespace FROCS.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppPersonFace",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialNumber = c.String(maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 128),
                        Position = c.String(maxLength: 128),
                        ImageUrl = c.String(),
                        FaceFeature = c.Binary(nullable: false),
                        Description = c.String(maxLength: 640),
                        CreationTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AppPersonFace");
        }
    }
}
