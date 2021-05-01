using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class seed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserStatus",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "UserStatus",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.InsertData(
                table: "UserType",
                columns: new[] { "Id", "Code", "DescriptionArabic", "DescriptionEnglish", "ValidFrom", "ValidTo" },
                values: new object[] { 1L, null, "Admin", "Admin", null, null });

            migrationBuilder.InsertData(
                table: "UserType",
                columns: new[] { "Id", "Code", "DescriptionArabic", "DescriptionEnglish", "ValidFrom", "ValidTo" },
                values: new object[] { 2L, null, "user", "user", null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserType",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "UserType",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.InsertData(
                table: "UserStatus",
                columns: new[] { "Id", "Code", "DescriptionArabic", "DescriptionEnglish", "ValidFrom", "ValidTo" },
                values: new object[] { 1L, null, "Admin", "Admin", null, null });

            migrationBuilder.InsertData(
                table: "UserStatus",
                columns: new[] { "Id", "Code", "DescriptionArabic", "DescriptionEnglish", "ValidFrom", "ValidTo" },
                values: new object[] { 2L, null, "user", "user", null, null });
        }
    }
}
