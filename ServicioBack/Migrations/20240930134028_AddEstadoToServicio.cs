using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicioBack.Migrations
{
    /// <inheritdoc />
    public partial class AddEstadoToServicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "T_Servicios",
                nullable: false,
                defaultValue: false);






            migrationBuilder.CreateIndex(
                name: "IX_T_DETALLES_TURNO_id_servicio",
                table: "T_DETALLES_TURNO",
                column: "id_servicio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropColumn(
                name: "Estado",
                table: "T_SERVICIOS");


        }
    }
}
