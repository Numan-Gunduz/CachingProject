using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cache.Migrations
{
    /// <inheritdoc />
    public partial class AddResetCacheStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE ResetCache
                    @TableName NVARCHAR(128)
                AS
                BEGIN
                    DECLARE @URL NVARCHAR(2048)
                    DECLARE @HttpRequest INT
                    DECLARE @HttpResponse INT
                    DECLARE @Status INT
                    DECLARE @ApiUrl NVARCHAR(2048) = 'http://localhost:5190/api/Cache/ResetCache?tableName'

                    -- HTTP URL oluşturma
                    SET @URL = @ApiUrl + '=' + @TableName

                    -- HTTP PUT isteği oluşturma
                    EXEC sp_OACreate 'MSXML2.ServerXMLHTTP', @HttpRequest OUT;
                    EXEC sp_OAMethod @HttpRequest, 'open', NULL, 'PUT', @URL, 'false';
                    EXEC sp_OAMethod @HttpRequest, 'setRequestHeader', NULL, 'Content-Type', 'application/json';
                    EXEC sp_OAMethod @HttpRequest, 'send', NULL;

                    -- HTTP isteğinin durumunu alma
                    EXEC sp_OAGetProperty @HttpRequest, 'status', @Status OUT;

                    IF @Status <> 200
                    BEGIN
                        RAISERROR('API request failed with status code: %d', 16, 1, @Status);
                    END

                    -- Nesneleri serbest bırakma
                    EXEC sp_OADestroy @HttpRequest;
                END
                GO
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS ResetCache;");
        }
    }
}
