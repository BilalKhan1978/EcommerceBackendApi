using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceBackendApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedEncryptedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PassHash",
                table: "Users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PassSalt",
                table: "Users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PassSalt",
                table: "Users");
        }
    }
}
