//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

//namespace Cache.Migrations
//{
//    /// <inheritdoc />
//    public partial class inital : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.CreateTable(
//                name: "Kategoris",
//                columns: table => new
//                {
//                    Id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    Isim = table.Column<string>(type: "nvarchar(max)", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Kategoris", x => x.Id);
//                });

//            migrationBuilder.CreateTable(
//                name: "Uruns",
//                columns: table => new
//                {
//                    Id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    Isim = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    KategoriId = table.Column<int>(type: "int", nullable: false),
//                    IsActive = table.Column<bool>(type: "bit", nullable: false)
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
//                table: "Kategoris",
//                columns: new[] { "Id", "Isim" },
//                values: new object[,]
//                {
//                    { 1, "Kategori-1" },
//                    { 2, "Kategori-2" },
//                    { 3, "Kategori-3" },
//                    { 4, "Kategori-4" },
//                    { 5, "Kategori-5" },
//                    { 6, "Kategori-6" },
//                    { 7, "Kategori-7" },
//                    { 8, "Kategori-8" },
//                    { 9, "Kategori-9" },
//                    { 10, "Kategori-10" }
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

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropTable(
//                name: "Uruns");

//            migrationBuilder.DropTable(
//                name: "Kategoris");
//        }
//    }
//}
