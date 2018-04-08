using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SoftwareEngineeringProject.Data.Migrations
{
    public partial class migration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyRequest_AspNetUsers_ApplicationUserId",
                table: "KeyRequest");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "KeyRequest",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_KeyRequest_ApplicationUserId",
                table: "KeyRequest",
                newName: "IX_KeyRequest_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyRequest_AspNetUsers_UserId",
                table: "KeyRequest",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyRequest_AspNetUsers_UserId",
                table: "KeyRequest");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "KeyRequest",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_KeyRequest_UserId",
                table: "KeyRequest",
                newName: "IX_KeyRequest_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyRequest_AspNetUsers_ApplicationUserId",
                table: "KeyRequest",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
