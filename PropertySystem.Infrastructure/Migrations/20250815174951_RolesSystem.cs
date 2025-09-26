﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertySystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RolesSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sys_role_user",
                columns: table => new
                {
                    roles_id = table.Column<int>(type: "int", nullable: false),
                    users_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_role_user", x => new { x.roles_id, x.users_id });
                    table.ForeignKey(
                        name: "fk_sys_role_user_sys_role_roles_id",
                        column: x => x.roles_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sys_role_user_user_users_id",
                        column: x => x.users_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[] { 1, "Registered" });

            migrationBuilder.CreateIndex(
                name: "ix_sys_role_user_users_id",
                table: "sys_role_user",
                column: "users_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_role_user");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
