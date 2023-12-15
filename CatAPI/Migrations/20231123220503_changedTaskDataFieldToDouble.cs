using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cat_a_logAPI.Migrations
{
    /// <inheritdoc />
    public partial class changedTaskDataFieldToDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "AutoProgress",
                table: "TaskData",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AutoProgress",
                table: "TaskData",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
