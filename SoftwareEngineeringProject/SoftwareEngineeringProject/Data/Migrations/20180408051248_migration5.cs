using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SoftwareEngineeringProject.Data.Migrations
{
    public partial class migration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_KeyRequestLines_KeyRequestLinesId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_KeyRequestLinesId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "KeyRequestLinesId",
                table: "Rooms");

            migrationBuilder.AddColumn<int>(
                name: "RoomsId",
                table: "KeyRequestLines",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KeyRequestLines_RoomsId",
                table: "KeyRequestLines",
                column: "RoomsId",
                unique: true,
                filter: "[RoomsId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyRequestLines_Rooms_RoomsId",
                table: "KeyRequestLines",
                column: "RoomsId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyRequestLines_Rooms_RoomsId",
                table: "KeyRequestLines");

            migrationBuilder.DropIndex(
                name: "IX_KeyRequestLines_RoomsId",
                table: "KeyRequestLines");

            migrationBuilder.DropColumn(
                name: "RoomsId",
                table: "KeyRequestLines");

            migrationBuilder.AddColumn<int>(
                name: "KeyRequestLinesId",
                table: "Rooms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_KeyRequestLinesId",
                table: "Rooms",
                column: "KeyRequestLinesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_KeyRequestLines_KeyRequestLinesId",
                table: "Rooms",
                column: "KeyRequestLinesId",
                principalTable: "KeyRequestLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
