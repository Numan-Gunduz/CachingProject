//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace Cache.Migrations
//{
//    /// <inheritdoc />
//    public partial class mig_3 : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.Sql(@"
//                CREATE TRIGGER UrunsTableChangeTrigger
//                ON Uruns
//                AFTER INSERT, UPDATE, DELETE
//                AS
//                BEGIN
//                    DECLARE @TableName NVARCHAR(128);

                    
//                    SELECT @TableName = OBJECT_NAME(parent_object_id)
//                    FROM sys.objects
//                    WHERE object_id = @@PROCID;

                    
//                    EXEC dbo.ResetCache @TableName = @TableName;
//                END
//            ");
//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.Sql("DROP TRIGGER UrunsTableChangeTrigger");
//        }
//    }
//}
