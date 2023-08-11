using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorCrud.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          



         migrationBuilder.CreateTable(
         name: "Mauro",
         columns: table => new
         {
             idMauro = table.Column<int>(type: "int", nullable: false)
                 .Annotation("SqlServer:Identity", "1, 1"),
             Apellido = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
         },
         constraints: table =>
         {
             table.PrimaryKey("PK__Mauro__787A433DFF5FBD82", x => x.idMauro);
         });


      

            migrationBuilder.DropTable(
                name: "Mauro");
        }
    }
}
