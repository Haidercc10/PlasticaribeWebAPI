using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class AgregarTipoID_Usuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Areas_Area_Id",
                table: "Usuarios"); 

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Cajas_Compensaciones_cajComp_Id",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Empresas_Empresa_Id",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_EPS_eps_Id",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Estados_Estado_Id",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_FondosPensiones_fPen_Id",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_Usuarios_RolUsu_Id",
                table: "Usuarios");*/

            /* migrationBuilder.DropForeignKey(
                 name: "FK_Usuarios_TipoIdentificaciones_TipoIdentificacion_Id",
                 table: "Usuarios"); */

            /* migrationBuilder.DropForeignKey(
                 name: "FK_Usuarios_Tipos_Usuarios_tpUsu_Id",
                 table: "Usuarios"); */


            /* migrationBuilder.DropIndex(
                 name: "IX_Usuarios_Area_Id",
                 table: "Usuarios");

             migrationBuilder.DropIndex(
                 name: "IX_Usuarios_cajComp_Id",
                 table: "Usuarios");

             migrationBuilder.DropIndex(
                 name: "IX_Usuarios_Empresa_Id",
                 table: "Usuarios");

             migrationBuilder.DropIndex(
                 name: "IX_Usuarios_eps_Id",
                 table: "Usuarios");

             migrationBuilder.DropIndex(
                 name: "IX_Usuarios_Estado_Id",
                 table: "Usuarios");

             migrationBuilder.DropIndex(
                 name: "IX_Usuarios_fPen_Id",
                 table: "Usuarios");

             migrationBuilder.DropIndex(
                 name: "IX_Usuarios_RolUsu_Id",
                 table: "Usuarios");

             migrationBuilder.DropIndex(
                 name: "IX_Usuarios_tpUsu_Id",
                 table: "Usuarios"); */

            /*migrationBuilder.RenameColumn(
                name: "Area_Id",
                table: "Usuarios",
                newName: "area_Id");*/


            /*migrationBuilder.AddColumn<long>(
                name: "Area_Id",
                table: "Usuarios",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Empresa_Id1",
                table: "Usuarios",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Estado_Id1",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RolUsu_Id1",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "cajComp_Id1",
                table: "Usuarios",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "eps_Id1",
                table: "Usuarios",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "fPen_Id1",
                table: "Usuarios",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "tpUsu_Id1",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0); */

            /*migrationBuilder.CreateIndex(
                 name: "IX_Usuarios_Area_Id",
                 table: "Usuarios",
                 column: "Area_Id");

             migrationBuilder.CreateIndex(
                 name: "IX_Usuarios_cajComp_Id1",
                 table: "Usuarios",
                 column: "cajComp_Id1");

             migrationBuilder.CreateIndex(
                 name: "IX_Usuarios_Empresa_Id1",
                 table: "Usuarios",
                 column: "Empresa_Id1");

             migrationBuilder.CreateIndex(
                 name: "IX_Usuarios_eps_Id1",
                 table: "Usuarios",
                 column: "eps_Id1");

             migrationBuilder.CreateIndex(
                 name: "IX_Usuarios_Estado_Id1",
                 table: "Usuarios",
                 column: "Estado_Id1");

             migrationBuilder.CreateIndex(
                 name: "IX_Usuarios_fPen_Id1",
                 table: "Usuarios",
                 column: "fPen_Id1");

             migrationBuilder.CreateIndex(
                 name: "IX_Usuarios_RolUsu_Id1",
                 table: "Usuarios",
                 column: "RolUsu_Id1");

             migrationBuilder.CreateIndex(
                 name: "IX_Usuarios_tpUsu_Id1",
                 table: "Usuarios",
                 column: "tpUsu_Id1"); */



            /*  migrationBuilder.AddForeignKey(
                  name: "FK_Usuarios_Areas_Area_Id",
                  table: "Usuarios",
                  column: "Area_Id",
                  principalTable: "Areas",
                  principalColumn: "Area_Id",
                  onDelete: ReferentialAction.Cascade); 

              migrationBuilder.AddForeignKey(
                  name: "FK_Usuarios_Cajas_Compensaciones_cajComp_Id1",
                  table: "Usuarios",
                  column: "cajComp_Id1",
                  principalTable: "Cajas_Compensaciones",
                  principalColumn: "cajComp_Id",
                  onDelete: ReferentialAction.Cascade);

              migrationBuilder.AddForeignKey(
                  name: "FK_Usuarios_Empresas_Empresa_Id1",
                  table: "Usuarios",
                  column: "Empresa_Id1",
                  principalTable: "Empresas",
                  principalColumn: "Empresa_Id",
                  onDelete: ReferentialAction.Cascade);

              migrationBuilder.AddForeignKey(
                  name: "FK_Usuarios_EPS_eps_Id1",
                  table: "Usuarios",
                  column: "eps_Id1",
                  principalTable: "EPS",
                  principalColumn: "eps_Id",
                  onDelete: ReferentialAction.Cascade);

              migrationBuilder.AddForeignKey(
                  name: "FK_Usuarios_Estados_Estado_Id1",
                  table: "Usuarios",
                  column: "Estado_Id1",
                  principalTable: "Estados",
                  principalColumn: "Estado_Id",
                  onDelete: ReferentialAction.Cascade);

              migrationBuilder.AddForeignKey(
                  name: "FK_Usuarios_FondosPensiones_fPen_Id1",
                  table: "Usuarios",
                  column: "fPen_Id1",
                  principalTable: "FondosPensiones",
                  principalColumn: "fPen_Id",
                  onDelete: ReferentialAction.Cascade);

              migrationBuilder.AddForeignKey(
                  name: "FK_Usuarios_Roles_Usuarios_RolUsu_Id1",
                  table: "Usuarios",
                  column: "RolUsu_Id1",
                  principalTable: "Roles_Usuarios",
                  principalColumn: "RolUsu_Id",
                  onDelete: ReferentialAction.Cascade); */

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "Usuarios",
                column: "TipoIdentificacion_Id",
                principalTable: "TipoIdentificaciones",
                principalColumn: "TipoIdentificacion_Id",
                onDelete: ReferentialAction.Restrict);

            /* migrationBuilder.AddForeignKey(
                 name: "FK_Usuarios_Tipos_Usuarios_tpUsu_Id1",
                 table: "Usuarios",
                 column: "tpUsu_Id1",
                 principalTable: "Tipos_Usuarios",
                 principalColumn: "tpUsu_Id",
                 onDelete: ReferentialAction.Cascade); */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /* migrationBuilder.DropForeignKey(
                 name: "FK_Usuarios_Areas_Area_Id",
                 table: "Usuarios");

             migrationBuilder.DropForeignKey(
                 name: "FK_Usuarios_Cajas_Compensaciones_cajComp_Id1",
                 table: "Usuarios");

             migrationBuilder.DropForeignKey(
                 name: "FK_Usuarios_Empresas_Empresa_Id1",
                 table: "Usuarios");

             migrationBuilder.DropForeignKey(
                 name: "FK_Usuarios_EPS_eps_Id1",
                 table: "Usuarios");

             migrationBuilder.DropForeignKey(
                 name: "FK_Usuarios_Estados_Estado_Id1",
                 table: "Usuarios");

             migrationBuilder.DropForeignKey(
                 name: "FK_Usuarios_FondosPensiones_fPen_Id1",
                 table: "Usuarios");

             migrationBuilder.DropForeignKey(
                 name: "FK_Usuarios_Roles_Usuarios_RolUsu_Id1",
                 table: "Usuarios"); */

            /*   migrationBuilder.DropForeignKey(
                   name: "FK_Usuarios_TipoIdentificaciones_TipoIdentificacion_Id",
                   table: "Usuarios"); */

            /*  migrationBuilder.DropForeignKey(
                  name: "FK_Usuarios_Tipos_Usuarios_tpUsu_Id1",
                  table: "Usuarios"); */



            /*   migrationBuilder.DropIndex(
                   name: "IX_Usuarios_Area_Id",
                   table: "Usuarios");

               migrationBuilder.DropIndex(
                   name: "IX_Usuarios_cajComp_Id1",
                   table: "Usuarios");

               migrationBuilder.DropIndex(
                   name: "IX_Usuarios_Empresa_Id1",
                   table: "Usuarios");

               migrationBuilder.DropIndex(
                   name: "IX_Usuarios_eps_Id1",
                   table: "Usuarios");

               migrationBuilder.DropIndex(
                   name: "IX_Usuarios_Estado_Id1",
                   table: "Usuarios");

               migrationBuilder.DropIndex(
                   name: "IX_Usuarios_fPen_Id1",
                   table: "Usuarios");

               migrationBuilder.DropIndex(
                   name: "IX_Usuarios_RolUsu_Id1",
                   table: "Usuarios");

               migrationBuilder.DropIndex(
                   name: "IX_Usuarios_tpUsu_Id1",
                   table: "Usuarios"); */

            /* migrationBuilder.DropColumn(
                 name: "Area_Id",
                 table: "Usuarios");

             migrationBuilder.DropColumn(
                 name: "Empresa_Id1",
                 table: "Usuarios");

             migrationBuilder.DropColumn(
                 name: "Estado_Id1",
                 table: "Usuarios");

             migrationBuilder.DropColumn(
                 name: "RolUsu_Id1",
                 table: "Usuarios");

             migrationBuilder.DropColumn(
                 name: "cajComp_Id1",
                 table: "Usuarios");

             migrationBuilder.DropColumn(
                 name: "eps_Id1",
                 table: "Usuarios");

             migrationBuilder.DropColumn(
                 name: "fPen_Id1",
                 table: "Usuarios");

             migrationBuilder.DropColumn(
                 name: "tpUsu_Id1",
                 table: "Usuarios");

             migrationBuilder.RenameColumn(
                 name: "area_Id",
                 table: "Usuarios",
                 newName: "Area_Id"); */

            /*  migrationBuilder.CreateIndex(
                  name: "IX_Usuarios_Area_Id",
                  table: "Usuarios",
                  column: "Area_Id");

              migrationBuilder.CreateIndex(
                  name: "IX_Usuarios_cajComp_Id",
                  table: "Usuarios",
                  column: "cajComp_Id");

              migrationBuilder.CreateIndex(
                  name: "IX_Usuarios_Empresa_Id",
                  table: "Usuarios",
                  column: "Empresa_Id");

              migrationBuilder.CreateIndex(
                  name: "IX_Usuarios_eps_Id",
                  table: "Usuarios",
                  column: "eps_Id");

              migrationBuilder.CreateIndex(
                  name: "IX_Usuarios_Estado_Id",
                  table: "Usuarios",
                  column: "Estado_Id");

              migrationBuilder.CreateIndex(
                  name: "IX_Usuarios_fPen_Id",
                  table: "Usuarios",
                  column: "fPen_Id");

              migrationBuilder.CreateIndex(
                  name: "IX_Usuarios_RolUsu_Id",
                  table: "Usuarios",
                  column: "RolUsu_Id");

              migrationBuilder.CreateIndex(
                  name: "IX_Usuarios_tpUsu_Id",
                  table: "Usuarios",
                  column: "tpUsu_Id"); */

            /* migrationBuilder.AddForeignKey(
                 name: "FK_Usuarios_Areas_Area_Id",
                 table: "Usuarios",
                 column: "Area_Id",
                 principalTable: "Areas",
                 principalColumn: "Area_Id",
                 onDelete: ReferentialAction.Cascade);

             migrationBuilder.AddForeignKey(
                 name: "FK_Usuarios_Cajas_Compensaciones_cajComp_Id",
                 table: "Usuarios",
                 column: "cajComp_Id",
                 principalTable: "Cajas_Compensaciones",
                 principalColumn: "cajComp_Id",
                 onDelete: ReferentialAction.Cascade);

             migrationBuilder.AddForeignKey(
                 name: "FK_Usuarios_Empresas_Empresa_Id",
                 table: "Usuarios",
                 column: "Empresa_Id",
                 principalTable: "Empresas",
                 principalColumn: "Empresa_Id",
                 onDelete: ReferentialAction.Cascade);

             migrationBuilder.AddForeignKey(
                 name: "FK_Usuarios_EPS_eps_Id",
                 table: "Usuarios",
                 column: "eps_Id",
                 principalTable: "EPS",
                 principalColumn: "eps_Id",
                 onDelete: ReferentialAction.Cascade);

             migrationBuilder.AddForeignKey(
                 name: "FK_Usuarios_Estados_Estado_Id",
                 table: "Usuarios",
                 column: "Estado_Id",
                 principalTable: "Estados",
                 principalColumn: "Estado_Id",
                 onDelete: ReferentialAction.Cascade);

             migrationBuilder.AddForeignKey(
                 name: "FK_Usuarios_FondosPensiones_fPen_Id",
                 table: "Usuarios",
                 column: "fPen_Id",
                 principalTable: "FondosPensiones",
                 principalColumn: "fPen_Id",
                 onDelete: ReferentialAction.Cascade);

             migrationBuilder.AddForeignKey(
                 name: "FK_Usuarios_Roles_Usuarios_RolUsu_Id",
                 table: "Usuarios",
                 column: "RolUsu_Id",
                 principalTable: "Roles_Usuarios",
                 principalColumn: "RolUsu_Id",
                 onDelete: ReferentialAction.Cascade); */

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "Usuarios",
                column: "TipoIdentificacion_Id",
                principalTable: "TipoIdentificaciones",
                principalColumn: "TipoIdentificacion_Id",
                onDelete: ReferentialAction.Cascade);

            /* migrationBuilder.AddForeignKey(
                 name: "FK_Usuarios_Tipos_Usuarios_tpUsu_Id",
                 table: "Usuarios",
                 column: "tpUsu_Id",
                 principalTable: "Tipos_Usuarios",
                 principalColumn: "tpUsu_Id",
                 onDelete: ReferentialAction.Cascade); */
        }
    }
}
