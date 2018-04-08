using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SoftwareEngineeringProject.Data.Migrations
{
    public partial class migration7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AlterColumn<string>(
                name: "RoomID",
                table: "KeyRequestLines",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddForeignKey(
                name: "FK_KeyRequestLines_Rooms_RoomID",
                table: "KeyRequestLines",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
