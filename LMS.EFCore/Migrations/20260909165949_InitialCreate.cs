using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblbooks",
                columns: table => new
                {
                    BookID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BookName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Author = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Publisher = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false, defaultValue: "AVAILABLE", collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedTimestamp = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedTimestamp = table.Column<DateTime>(type: "timestamp", rowVersion: true, nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblbooks", x => x.BookID);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "tblmembers",
                columns: table => new
                {
                    MemberID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MemberName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedTimestamp = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedTimestamp = table.Column<DateTime>(type: "timestamp", rowVersion: true, nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblmembers", x => x.MemberID);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "tblborrows",
                columns: table => new
                {
                    BorrowID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MemberID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BookID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BorrowStartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    BorrowEndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsReturned = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedTimestamp = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedTimestamp = table.Column<DateTime>(type: "timestamp", rowVersion: true, nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblborrows", x => x.BorrowID);
                    table.ForeignKey(
                        name: "FK_tblborrows_tblbooks_BookID",
                        column: x => x.BookID,
                        principalTable: "tblbooks",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblborrows_tblmembers_MemberID",
                        column: x => x.MemberID,
                        principalTable: "tblmembers",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateIndex(
                name: "IX_tblborrows_BookID",
                table: "tblborrows",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_tblborrows_MemberID",
                table: "tblborrows",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_tblmembers_PhoneNo_Email",
                table: "tblmembers",
                columns: new[] { "PhoneNo", "Email" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblborrows");

            migrationBuilder.DropTable(
                name: "tblbooks");

            migrationBuilder.DropTable(
                name: "tblmembers");
        }
    }
}
