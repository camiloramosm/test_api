using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertySystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LoginProcess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "identity_id",
                table: "user",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_user_identity_id",
                table: "user",
                column: "identity_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_user_identity_id",
                table: "user");

            migrationBuilder.DropColumn(
                name: "identity_id",
                table: "user");
        }
    }
}
