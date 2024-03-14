using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class AdicionCampo_FacturasInverGoalSuez : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Restar_DashboardCostos",
                table: "Facturas_Invergoal_Inversuez",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Restar_DashboardCostos",
                table: "Facturas_Invergoal_Inversuez");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
