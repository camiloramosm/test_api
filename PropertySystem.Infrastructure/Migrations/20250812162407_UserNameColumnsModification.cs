using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertySystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserNameColumnsModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_user_identity_id",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "inactiovation_date",
                table: "user",
                newName: "inactivation_date");

            migrationBuilder.AlterColumn<bool>(
                name: "collect_shipping",
                table: "payment_intention",
                type: "bit",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_identity_id",
                table: "user",
                column: "identity_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_user_identity_id",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "inactivation_date",
                table: "user",
                newName: "inactiovation_date");

            migrationBuilder.AlterColumn<string>(
                name: "collect_shipping",
                table: "payment_intention",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_identity_id",
                table: "user",
                column: "identity_id",
                unique: true);
        }
    }
}
