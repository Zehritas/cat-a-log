using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatAPI.Migrations
{
    /// <inheritdoc />
    public partial class removedDependencyDuplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dependency_TaskData_PredecessorTaskId",
                table: "Dependency");

            migrationBuilder.DropForeignKey(
                name: "FK_Dependency_TaskData_SuccessorTaskId",
                table: "Dependency");

            migrationBuilder.AddForeignKey(
                name: "FK_Dependency_TaskData_PredecessorTaskId",
                table: "Dependency",
                column: "PredecessorTaskId",
                principalTable: "TaskData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dependency_TaskData_SuccessorTaskId",
                table: "Dependency",
                column: "SuccessorTaskId",
                principalTable: "TaskData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dependency_TaskData_PredecessorTaskId",
                table: "Dependency");

            migrationBuilder.DropForeignKey(
                name: "FK_Dependency_TaskData_SuccessorTaskId",
                table: "Dependency");

            migrationBuilder.AddForeignKey(
                name: "FK_Dependency_TaskData_PredecessorTaskId",
                table: "Dependency",
                column: "PredecessorTaskId",
                principalTable: "TaskData",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dependency_TaskData_SuccessorTaskId",
                table: "Dependency",
                column: "SuccessorTaskId",
                principalTable: "TaskData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
