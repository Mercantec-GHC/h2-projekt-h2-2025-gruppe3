using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Hotels",
                newName: "Zip");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Phone",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Booked",
                table: "Rooms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Crib",
                table: "Rooms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Extrabed",
                table: "Rooms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Rooms",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Hotels",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Hotels",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Facility",
                table: "Hotels",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Hotels",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Road",
                table: "Hotels",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "Booking",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Booked",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Crib",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Extrabed",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Facility",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Road",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "Zip",
                table: "Hotels",
                newName: "Address");

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Roles",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "1",
                column: "Description",
                value: "Standard bruger med basis rettigheder");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "2",
                column: "Description",
                value: "Rengøringspersonale med adgang til rengøringsmoduler");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3",
                column: "Description",
                value: "Receptionspersonale med adgang til booking og gæster");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "4",
                column: "Description",
                value: "Administrator med fuld adgang til systemet");
        }
    }
}
