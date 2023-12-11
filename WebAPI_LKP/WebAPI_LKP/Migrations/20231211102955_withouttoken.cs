using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_LKP.Migrations
{
    /// <inheritdoc />
    public partial class withouttoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tokens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    JwtToken = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.JwtToken);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }
    }
}
