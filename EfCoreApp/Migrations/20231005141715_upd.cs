using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCoreApp.Migrations
{
    /// <inheritdoc />
    public partial class upd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OgrencitId",
                table: "KursKayitlari",
                newName: "OgrenciId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OgrenciId",
                table: "KursKayitlari",
                newName: "OgrencitId");
        }
    }
}
