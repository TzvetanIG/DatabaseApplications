namespace StudentSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Courses", "Discription", c => c.String(maxLength: 250));
            AlterColumn("dbo.Homework", "Content", c => c.String(maxLength: 300));
            AlterColumn("dbo.Students", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Students", "PhoneNember", c => c.String(maxLength: 20));
            AlterColumn("dbo.Resources", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Resources", "Link", c => c.String(maxLength: 250));
            DropColumn("dbo.Resources", "Discription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Resources", "Discription", c => c.String());
            AlterColumn("dbo.Resources", "Link", c => c.String());
            AlterColumn("dbo.Resources", "Name", c => c.String());
            AlterColumn("dbo.Students", "PhoneNember", c => c.String());
            AlterColumn("dbo.Students", "Name", c => c.String());
            AlterColumn("dbo.Homework", "Content", c => c.String());
            AlterColumn("dbo.Courses", "Discription", c => c.String());
            AlterColumn("dbo.Courses", "Name", c => c.String());
        }
    }
}
