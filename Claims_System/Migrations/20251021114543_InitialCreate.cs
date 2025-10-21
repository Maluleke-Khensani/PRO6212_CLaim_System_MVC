using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Claims_System.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LecturerClaims",
                columns: table => new
                {
                    ClaimId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    ModuleName = table.Column<string>(type: "TEXT", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Submitted = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HoursWorked = table.Column<decimal>(type: "REAL", nullable: false),
                    Rate = table.Column<decimal>(type: "REAL", nullable: false),
                    CoordinatorStatus = table.Column<string>(type: "TEXT", nullable: false),
                    ManagerStatus = table.Column<string>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: false),
                    Document1FileName = table.Column<string>(type: "TEXT", nullable: true),
                    Document1FileData = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Document2FileName = table.Column<string>(type: "TEXT", nullable: true),
                    Document2FileData = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturerClaims", x => x.ClaimId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    EmployeeNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "EmployeeNumber", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 1, 1001, "pass123", "lecturer1" },
                    { 2, 2001, "admin123", "coordinator1" },
                    { 3, 1002, "pass456", "lecturer2" },
                    { 4, 3001, "manager123", "manager1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LecturerClaims");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
