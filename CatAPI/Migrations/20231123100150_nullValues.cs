using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatAPI.Migrations
{
    /// <inheritdoc />
    public partial class nullValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskData_ProjectMilestone_MilestoneId",
                table: "TaskData");

            migrationBuilder.AlterColumn<int>(
                name: "MilestoneId",
                table: "TaskData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskData_ProjectMilestone_MilestoneId",
                table: "TaskData",
                column: "MilestoneId",
                principalTable: "ProjectMilestone",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskData_ProjectMilestone_MilestoneId",
                table: "TaskData");

            migrationBuilder.AlterColumn<int>(
                name: "MilestoneId",
                table: "TaskData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskData_ProjectMilestone_MilestoneId",
                table: "TaskData",
                column: "MilestoneId",
                principalTable: "ProjectMilestone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
