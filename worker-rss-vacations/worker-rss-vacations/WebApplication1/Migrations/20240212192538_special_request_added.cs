using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class special_request_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "special_requests",
                columns: table => new
                {
                    SpecialRequest_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateInitial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFinally = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestsDays = table.Column<double>(type: "float", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    FK_User_Id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FK_Conjunct_Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FK_TypeSpecialRequest_Id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State_Id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_special_requests", x => x.SpecialRequest_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "special_requests");
        }
    }
}
