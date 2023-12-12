using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cat_a_logAPI.Migrations
{
    /// <inheritdoc />
    public partial class correction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dependency_TaskData_DependeeTaskId",
                table: "Dependency");

            migrationBuilder.DropForeignKey(
                name: "FK_Dependency_TaskData_DependentTaskId",
                table: "Dependency");

            migrationBuilder.DropIndex(
                name: "IX_Dependency_DependeeTaskId",
                table: "Dependency");

            migrationBuilder.RenameColumn(
                name: "DependentTaskName",
                table: "Dependency",
                newName: "SuccessorTaskName");

            migrationBuilder.RenameColumn(
                name: "DependentTaskId",
                table: "Dependency",
                newName: "SuccessorTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Dependency_DependentTaskId",
                table: "Dependency",
                newName: "IX_Dependency_SuccessorTaskId");

            migrationBuilder.AddColumn<DateTime>(
                name: "TargetDate",
                table: "ProjectMilestone",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "DependeeTaskId",
                table: "Dependency",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PredecessorTaskId",
                table: "Dependency",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Dependency_PredecessorTaskId",
                table: "Dependency",
                column: "PredecessorTaskId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dependency_TaskData_PredecessorTaskId",
                table: "Dependency");

            migrationBuilder.DropForeignKey(
                name: "FK_Dependency_TaskData_SuccessorTaskId",
                table: "Dependency");

            migrationBuilder.DropIndex(
                name: "IX_Dependency_PredecessorTaskId",
                table: "Dependency");

            migrationBuilder.DropColumn(
                name: "TargetDate",
                table: "ProjectMilestone");

            migrationBuilder.DropColumn(
                name: "PredecessorTaskId",
                table: "Dependency");

            migrationBuilder.RenameColumn(
                name: "SuccessorTaskName",
                table: "Dependency",
                newName: "DependentTaskName");

            migrationBuilder.RenameColumn(
                name: "SuccessorTaskId",
                table: "Dependency",
                newName: "DependentTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Dependency_SuccessorTaskId",
                table: "Dependency",
                newName: "IX_Dependency_DependentTaskId");

            migrationBuilder.AlterColumn<int>(
                name: "DependeeTaskId",
                table: "Dependency",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dependency_DependeeTaskId",
                table: "Dependency",
                column: "DependeeTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dependency_TaskData_DependeeTaskId",
                table: "Dependency",
                column: "DependeeTaskId",
                principalTable: "TaskData",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dependency_TaskData_DependentTaskId",
                table: "Dependency",
                column: "DependentTaskId",
                principalTable: "TaskData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
