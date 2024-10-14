//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

//namespace Cache.Migrations.Cache
//{
//    /// <inheritdoc />
//    public partial class UpdateCacheContext : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropTable(
//                name: "Uruns");
//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.CreateTable(
//                name: "Uruns",
//                columns: table => new
//                {
//                    Id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    KategoriId = table.Column<int>(type: "int", nullable: false),
//                    IsActive = table.Column<bool>(type: "bit", nullable: false),
//                    Isim = table.Column<string>(type: "nvarchar(max)", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Uruns", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_Uruns_Kategoris_KategoriId",
//                        column: x => x.KategoriId,
//                        principalTable: "Kategoris",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.InsertData(
//                table: "Uruns",
//                columns: new[] { "Id", "IsActive", "Isim", "KategoriId" },
//                values: new object[,]
//                {
//                    { 1, true, "Urun-1", 1 },
//                    { 2, true, "Urun-2", 2 },
//                    { 3, true, "Urun-3", 3 },
//                    { 4, true, "Urun-4", 4 },
//                    { 5, true, "Urun-5", 5 },
//                    { 6, true, "Urun-6", 6 },
//                    { 7, true, "Urun-7", 7 },
//                    { 8, true, "Urun-8", 8 },
//                    { 9, true, "Urun-9", 9 },
//                    { 10, true, "Urun-10", 10 }
//                });

//            migrationBuilder.CreateIndex(
//                name: "IX_Uruns_KategoriId",
//                table: "Uruns",
//                column: "KategoriId");
//        }
//    }
//}
