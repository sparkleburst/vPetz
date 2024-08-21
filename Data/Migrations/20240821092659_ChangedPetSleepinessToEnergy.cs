using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vPetz.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPetSleepinessToEnergy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sleepiness",
                table: "Pets",
                newName: "Energy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Energy",
                table: "Pets",
                newName: "Sleepiness");
        }
    }
}
