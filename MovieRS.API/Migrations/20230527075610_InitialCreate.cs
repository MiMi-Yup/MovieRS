using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRS.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COUNTRY",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Name_Vi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__COUNTRY__3214EC07DB3E63CF", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MOVIE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTmdb = table.Column<int>(type: "int", nullable: true),
                    IdImdb = table.Column<int>(type: "int", nullable: true),
                    YearRelease = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MOVIE__3214EC07CC466EE7", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PARAMS",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PARAMS__3214EC0707E93F9A", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "USER_",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Password = table.Column<byte[]>(type: "binary(32)", fixedLength: true, maxLength: 32, nullable: true),
                    CountryId = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__USER___3214EC07EA9A7E47", x => x.Id);
                    table.ForeignKey(
                        name: "FkUSERCountry",
                        column: x => x.CountryId,
                        principalTable: "COUNTRY",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FAVOURITE",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PkFavorite", x => new { x.UserId, x.MovieId });
                    table.ForeignKey(
                        name: "FkFavoriteMovie",
                        column: x => x.MovieId,
                        principalTable: "MOVIE",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FkFavoriteUser",
                        column: x => x.UserId,
                        principalTable: "USER_",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HISTORY",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PkHistory", x => new { x.UserId, x.MovieId });
                    table.ForeignKey(
                        name: "FkHistory",
                        column: x => x.MovieId,
                        principalTable: "MOVIE",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FkHistoryUser",
                        column: x => x.UserId,
                        principalTable: "USER_",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "REVIEW",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime", nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__REVIEW__3214EC078461CE60", x => x.Id);
                    table.ForeignKey(
                        name: "FkReviewMovie",
                        column: x => x.MovieId,
                        principalTable: "MOVIE",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FkReviewUser",
                        column: x => x.UserId,
                        principalTable: "USER_",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FAVOURITE_MovieId",
                table: "FAVOURITE",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_HISTORY_MovieId",
                table: "HISTORY",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_REVIEW_MovieId",
                table: "REVIEW",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_REVIEW_UserId",
                table: "REVIEW",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_USER__CountryId",
                table: "USER_",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "UQ__USER___A9D105342DB23183",
                table: "USER_",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FAVOURITE");

            migrationBuilder.DropTable(
                name: "HISTORY");

            migrationBuilder.DropTable(
                name: "PARAMS");

            migrationBuilder.DropTable(
                name: "REVIEW");

            migrationBuilder.DropTable(
                name: "MOVIE");

            migrationBuilder.DropTable(
                name: "USER_");

            migrationBuilder.DropTable(
                name: "COUNTRY");
        }
    }
}
