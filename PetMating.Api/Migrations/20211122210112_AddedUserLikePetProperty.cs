using Microsoft.EntityFrameworkCore.Migrations;

namespace PetMating.Api.Migrations
{
    public partial class AddedUserLikePetProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFavourite",
                table: "UserLikePet",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavourite",
                table: "UserLikePet");
        }
    }
}
