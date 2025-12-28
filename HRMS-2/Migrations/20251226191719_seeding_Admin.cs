using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS_2.Migrations
{
    /// <inheritdoc />
    public partial class seeding_Admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "HashedPassword", "IsAdmid", "UserName" },
                values: new object[] { 1L, "$2a$11$kUuAowumTUPGmbOr6lnAvOcZ.hbUNA7g46FG/DirKWB7ILjJcAAQq", true, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
