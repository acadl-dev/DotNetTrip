using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetTrip.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PacoteTuristico",
                newName: "id_pacote");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Destino",
                newName: "id_destino");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Clientes",
                newName: "id_cliente");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId1",
                table: "Reserva",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PacoteTuristicoId1",
                table: "Reserva",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "PacoteTuristico",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Pais",
                table: "Destino",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "Destino",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Clientes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_ClienteId1",
                table: "Reserva",
                column: "ClienteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_PacoteTuristicoId1",
                table: "Reserva",
                column: "PacoteTuristicoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_Clientes_ClienteId1",
                table: "Reserva",
                column: "ClienteId1",
                principalTable: "Clientes",
                principalColumn: "id_cliente");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_PacoteTuristico_PacoteTuristicoId1",
                table: "Reserva",
                column: "PacoteTuristicoId1",
                principalTable: "PacoteTuristico",
                principalColumn: "id_pacote");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_Clientes_ClienteId1",
                table: "Reserva");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_PacoteTuristico_PacoteTuristicoId1",
                table: "Reserva");

            migrationBuilder.DropIndex(
                name: "IX_Reserva_ClienteId1",
                table: "Reserva");

            migrationBuilder.DropIndex(
                name: "IX_Reserva_PacoteTuristicoId1",
                table: "Reserva");

            migrationBuilder.DropColumn(
                name: "ClienteId1",
                table: "Reserva");

            migrationBuilder.DropColumn(
                name: "PacoteTuristicoId1",
                table: "Reserva");

            migrationBuilder.RenameColumn(
                name: "id_pacote",
                table: "PacoteTuristico",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id_destino",
                table: "Destino",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id_cliente",
                table: "Clientes",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "PacoteTuristico",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Pais",
                table: "Destino",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "Destino",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
