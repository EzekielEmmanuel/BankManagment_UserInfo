using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EF.Migrations.DataContext;

public partial class BankAccountModel : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "BankAccounts",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<string>("nvarchar(max)", nullable: false),
                Number = table.Column<string>("nvarchar(450)", nullable: false),
                Type = table.Column<string>("nvarchar(max)", nullable: false),
                MetaAddedDate = table.Column<DateTimeOffset>("datetimeoffset", nullable: false),
                MetaAddedUser = table.Column<string>("nvarchar(max)", nullable: false),
                MetaModifiedUser = table.Column<string>("nvarchar(max)", nullable: true),
                MetaModifiedDate = table.Column<DateTimeOffset>("datetimeoffset", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_BankAccounts", x => x.Id); });

        migrationBuilder.CreateIndex(
            "IX_BankAccounts_Number",
            "BankAccounts",
            "Number",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "BankAccounts");
    }
}