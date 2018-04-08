using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SoftwareEngineeringProject.Data.Migrations
{
    public partial class migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KeyRequestLinesId",
                table: "Rooms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReasonForAccess",
                table: "KeyRequestLines",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "KeyRequest",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_KeyRequestLinesId",
                table: "Rooms",
                column: "KeyRequestLinesId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyRequestLines_KeyRequestId",
                table: "KeyRequestLines",
                column: "KeyRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyRequest_ApplicationUserId",
                table: "KeyRequest",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyRequest_AspNetUsers_ApplicationUserId",
                table: "KeyRequest",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KeyRequestLines_KeyRequest_KeyRequestId",
                table: "KeyRequestLines",
                column: "KeyRequestId",
                principalTable: "KeyRequest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_KeyRequestLines_KeyRequestLinesId",
                table: "Rooms",
                column: "KeyRequestLinesId",
                principalTable: "KeyRequestLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyRequest_AspNetUsers_ApplicationUserId",
                table: "KeyRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_KeyRequestLines_KeyRequest_KeyRequestId",
                table: "KeyRequestLines");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_KeyRequestLines_KeyRequestLinesId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_KeyRequestLinesId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_KeyRequestLines_KeyRequestId",
                table: "KeyRequestLines");

            migrationBuilder.DropIndex(
                name: "IX_KeyRequest_ApplicationUserId",
                table: "KeyRequest");

            migrationBuilder.DropColumn(
                name: "KeyRequestLinesId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ReasonForAccess",
                table: "KeyRequestLines");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "KeyRequest");
        }
    }
}
