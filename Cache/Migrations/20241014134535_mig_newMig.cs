//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

//namespace Cache.Migrations
//{
//    /// <inheritdoc />
//    public partial class mig_newMig : Migration
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
//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropTable(
//                name: "Kategoris");
//        }
//    }
//}
