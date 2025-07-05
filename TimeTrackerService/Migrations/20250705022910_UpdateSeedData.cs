using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TimeTrackerService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Programming", "Programming" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Testing", "Testing" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Survey and assess the construction site", "Site Survey" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Pour and finish concrete structures", "Concrete Pouring" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 3, "Build structural framework", "Framing" },
                    { 4, "Install electrical systems", "Electrical Installation" },
                    { 5, "Perform final quality inspections", "Final Inspection" }
                });
        }
    }
}
