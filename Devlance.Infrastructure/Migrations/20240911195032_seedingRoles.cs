using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Devlance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5dd4c6b0-3caa-4604-80b0-659dd8fd7791", "450fdf8c-a283-4dac-ab65-ba6b050ce68c", "client", "CLIENT" },
                    { "8d225c69-02e5-4268-94c1-73e389b2df82", "5854f1e3-0638-4b64-88de-1531c54a5746", "freelancer", "FREELANCER" },
                    { "ae814c6a-6434-4fee-9bc2-1d23fa950d4b", "0ddcbf61-4ca7-4742-bf7e-1997b21cdad3", "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dd4c6b0-3caa-4604-80b0-659dd8fd7791");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d225c69-02e5-4268-94c1-73e389b2df82");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae814c6a-6434-4fee-9bc2-1d23fa950d4b");
        }
    }
}
