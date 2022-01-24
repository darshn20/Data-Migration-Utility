using Microsoft.EntityFrameworkCore.Migrations;

namespace DataMigrationUtility.Data.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DestinationTable_SourceTable_SourceTableID",
                table: "DestinationTable");

            migrationBuilder.DropIndex(
                name: "IX_DestinationTable_SourceTableID",
                table: "DestinationTable");

            migrationBuilder.AlterColumn<int>(
                name: "SourceTableID",
                table: "DestinationTable",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SourceTableID",
                table: "DestinationTable",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_DestinationTable_SourceTableID",
                table: "DestinationTable",
                column: "SourceTableID");

            migrationBuilder.AddForeignKey(
                name: "FK_DestinationTable_SourceTable_SourceTableID",
                table: "DestinationTable",
                column: "SourceTableID",
                principalTable: "SourceTable",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
