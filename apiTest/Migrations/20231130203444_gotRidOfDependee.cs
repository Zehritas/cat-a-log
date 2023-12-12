using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cat_a_logB.Migrations
{
    /// <inheritdoc />
    public partial class gotRidOfDependee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DependeeTaskId",
                table: "Dependency");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DependeeTaskId",
                table: "Dependency",
                type: "int",
                nullable: true);
        }
    }
}
