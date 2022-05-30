using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HomeJok.Repository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    RealName = table.Column<string>(type: "text", nullable: true),
                    NickName = table.Column<string>(type: "text", nullable: true),
                    Avatar = table.Column<string>(type: "text", nullable: true),
                    Sex = table.Column<int>(type: "integer", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Birthday = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    ActiveState = table.Column<bool>(type: "boolean", nullable: false),
                    LastLoginTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreateUser = table.Column<int>(type: "integer", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateUser = table.Column<int>(type: "integer", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInfo");
        }
    }
}
