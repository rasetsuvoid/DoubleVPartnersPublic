using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FourMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "documenTypes",
                columns: new[] { "Id", "Active", "CreatedDate", "IsDeleted", "Name", "UpdateDate" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2022, 11, 20, 19, 3, 21, 628, DateTimeKind.Local).AddTicks(3624), false, "registro civil de nacimiento", null },
                    { 2, true, new DateTime(2022, 11, 20, 19, 3, 21, 628, DateTimeKind.Local).AddTicks(3627), false, "cédula de ciudadanía", null },
                    { 3, true, new DateTime(2022, 11, 20, 19, 3, 21, 628, DateTimeKind.Local).AddTicks(3628), false, "registro civil de nacimiento", null },
                    { 4, true, new DateTime(2022, 11, 20, 19, 3, 21, 628, DateTimeKind.Local).AddTicks(3630), false, "tarjeta de identidad", null },
                    { 5, true, new DateTime(2022, 11, 20, 19, 3, 21, 628, DateTimeKind.Local).AddTicks(3631), false, "tarjeta de extranjería", null },
                    { 6, true, new DateTime(2022, 11, 20, 19, 3, 21, 628, DateTimeKind.Local).AddTicks(3633), false, "cédula de extranjer", null },
                    { 7, true, new DateTime(2022, 11, 20, 19, 3, 21, 628, DateTimeKind.Local).AddTicks(3634), false, "NIT", null },
                    { 8, true, new DateTime(2022, 11, 20, 19, 3, 21, 628, DateTimeKind.Local).AddTicks(3635), false, "Pasaporte", null },
                    { 9, true, new DateTime(2022, 11, 20, 19, 3, 21, 628, DateTimeKind.Local).AddTicks(3637), false, "tipo de documento extranjero", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "documenTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "documenTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "documenTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "documenTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "documenTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "documenTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "documenTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "documenTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "documenTypes",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
