using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Migrations
{
    /// <inheritdoc />
    public partial class SecondaryDatabaseSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoList_User_UserID",
                table: "TodoList");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "TodoList",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TodoList_UserID",
                table: "TodoList",
                newName: "IX_TodoList_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TodoList",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoList_AspNetUsers_UserId",
                table: "TodoList",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoList_AspNetUsers_UserId",
                table: "TodoList");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TodoList",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_TodoList_UserId",
                table: "TodoList",
                newName: "IX_TodoList_UserID");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "TodoList",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TodoList_User_UserID",
                table: "TodoList",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
