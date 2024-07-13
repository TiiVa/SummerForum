using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SummerForum.Api.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discussions_Departments_DepartmentId",
                table: "Discussions");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Posts_PostId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_PostId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Replies");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "BelongsToPostId",
                table: "Replies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Discussions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_BelongsToPostId",
                table: "Replies",
                column: "BelongsToPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussions_Departments_DepartmentId",
                table: "Discussions",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Posts_BelongsToPostId",
                table: "Replies",
                column: "BelongsToPostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discussions_Departments_DepartmentId",
                table: "Discussions");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Posts_BelongsToPostId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_BelongsToPostId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "BelongsToPostId",
                table: "Replies");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Replies",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Discussions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_PostId",
                table: "Replies",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussions_Departments_DepartmentId",
                table: "Discussions",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Posts_PostId",
                table: "Replies",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
