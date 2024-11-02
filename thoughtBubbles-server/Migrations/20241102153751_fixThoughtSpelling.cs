using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace thoughtBubbles_server.Migrations
{
    /// <inheritdoc />
    public partial class fixThoughtSpelling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "thought",
                table: "ThoughtBubbles",
                newName: "Thought");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Thought",
                table: "ThoughtBubbles",
                newName: "thought");
        }
    }
}
