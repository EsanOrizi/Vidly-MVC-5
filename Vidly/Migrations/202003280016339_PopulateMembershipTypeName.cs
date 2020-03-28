namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMembershipTypeName : DbMigration
    {
        public override void Up()
        {
            // sql statement to populate MembershipTypeName 
            Sql("UPDATE MembershipTypes SET MembershipTypeName = 'Pay as You Go' WHERE Id = 1");
            Sql("UPDATE MembershipTypes SET MembershipTypeName = 'Monthly' WHERE Id = 2");
            Sql("UPDATE MembershipTypes SET MembershipTypeName = 'Quarterly' WHERE Id = 3");
            Sql("UPDATE MembershipTypes SET MembershipTypeName = 'Anual' WHERE Id = 4");

        }

        public override void Down()
        {
        }
    }
}
