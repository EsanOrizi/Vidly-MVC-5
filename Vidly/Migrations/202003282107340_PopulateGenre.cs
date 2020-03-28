namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenre : DbMigration
    {
        public override void Up()
        {

            // to fix this error "Cannot insert explicit value for identity column in table 'Genres' when IDENTITY_INSERT is set to OFF."
            Sql("SET IDENTITY_INSERT Genres ON");
            // insert genre into genres table
            Sql("INSERT INTO Genres (Id, Name) VALUES (1, 'Action')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (2, 'Thriller')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (3, 'Family')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (4, 'Romance')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (5, 'Comedy')");
        }
        
        public override void Down()
        {
        }
    }
}
