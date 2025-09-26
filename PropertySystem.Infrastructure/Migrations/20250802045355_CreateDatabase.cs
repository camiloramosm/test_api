using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertySystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "payment_intention",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status_email = table.Column<bool>(type: "bit", nullable: true),
                    dynamic_value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    collect_shipping = table.Column<bool>(type: "bit", nullable: true),
                    single_use = table.Column<bool>(type: "bit", nullable: true),
                    transaction_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    money_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    money_currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    redirect_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    business_unit = table.Column<int>(type: "int", nullable: false),
                    app = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    wallet = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    document = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    document_payment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_session = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    token_session = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    expires_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    client_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    client_branch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    client_nit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    client_phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    client_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    supplier_message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    version = table.Column<long>(type: "bigint", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_intention", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "varchar(30)", nullable: false),
                    second_name = table.Column<string>(type: "varchar(30)", nullable: false),
                    last_name = table.Column<string>(type: "varchar(30)", nullable: false),
                    full_name = table.Column<string>(type: "varchar(100)", nullable: false),
                    user_contact_phone = table.Column<string>(type: "varchar(15)", nullable: false),
                    user_contact_address = table.Column<string>(type: "varchar(200)", nullable: false),
                    business_unit = table.Column<int>(type: "int", nullable: false),
                    password = table.Column<string>(type: "varchar(50)", nullable: false),
                    role = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    identification = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    inactiovation_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    email = table.Column<string>(type: "varchar(400)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_email",
                table: "user",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payment_intention");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
