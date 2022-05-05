using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseLayer.Migrations
{
    public partial class FixNormalForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.Sql(
                @"
                    UPDATE Customer
                    SET FirstName = trim(left(Name, charindex(',', Name)-1));
                ");
            migrationBuilder.Sql(
                @"
                    UPDATE Customer
                    SET LastName = trim(right(Name, len(Name)-charindex(',', Name)));
                ");

            migrationBuilder.DropColumn("Name", "Customer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.Sql(
                @"
                    UPDATE Customer
                    SET Name = FirstName +', '+ LastName;
                ");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Customer");
        }
    }
}
