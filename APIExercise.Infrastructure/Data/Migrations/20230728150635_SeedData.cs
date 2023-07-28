using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIExercise.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Discriminator", "FirstName", "IdDocument", "LastName", "Password", "PhoneNumber", "Status" },
                values: new object[,]
                {
                    { new Guid("1cfec38a-5cc2-4aa9-9775-9425494d359c"), "Client", "Marianela", "91011121", "Montalvo", "5678", "097548965", 2 },
                    { new Guid("43215da9-8700-457b-83cd-e2b9ffc63468"), "Client", "Juan", "31415161", "Osorio", "1245", "098874587", 2 },
                    { new Guid("8df0f660-ec84-4295-83ea-4ccede958993"), "Client", "Jose", "12345678", "Lema", "1234", "098254785", 2 }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "AccountType", "Balance", "ClientId", "InitialBalance", "Status" },
                values: new object[,]
                {
                    { new Guid("a0bbf2c2-cae5-4108-8fe0-a2364e2579ff"), "496825", 2, 540m, new Guid("1cfec38a-5cc2-4aa9-9775-9425494d359c"), 0m, 2 },
                    { new Guid("c12aa5d0-6f53-4bf8-8148-4ba498fd7127"), "478758", 1, 2000m, new Guid("8df0f660-ec84-4295-83ea-4ccede958993"), 0m, 2 },
                    { new Guid("c983e8c3-6487-4b47-9ff6-5ee3aad89de0"), "495878", 1, 0m, new Guid("43215da9-8700-457b-83cd-e2b9ffc63468"), 0m, 2 },
                    { new Guid("fe27f0f2-22ca-4592-9c65-922f62b1ae19"), "225487", 2, 100m, new Guid("1cfec38a-5cc2-4aa9-9775-9425494d359c"), 0m, 2 }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "PostalCode", "State", "Street" },
                values: new object[,]
                {
                    { new Guid("1cfec38a-5cc2-4aa9-9775-9425494d359c"), "Santiago de Surco", "Perú", "15023", "Lima", "Avenida Caminos del Inca" },
                    { new Guid("43215da9-8700-457b-83cd-e2b9ffc63468"), "La Molina", "Perú", "15026", "Lima", "Avenida La Molina" },
                    { new Guid("8df0f660-ec84-4295-83ea-4ccede958993"), "Lima", "Perú", "15001", "Lima", "Jirón de la Unión" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("a0bbf2c2-cae5-4108-8fe0-a2364e2579ff"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("c12aa5d0-6f53-4bf8-8148-4ba498fd7127"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("c983e8c3-6487-4b47-9ff6-5ee3aad89de0"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("fe27f0f2-22ca-4592-9c65-922f62b1ae19"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("1cfec38a-5cc2-4aa9-9775-9425494d359c"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("43215da9-8700-457b-83cd-e2b9ffc63468"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("8df0f660-ec84-4295-83ea-4ccede958993"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: new Guid("1cfec38a-5cc2-4aa9-9775-9425494d359c"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: new Guid("43215da9-8700-457b-83cd-e2b9ffc63468"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: new Guid("8df0f660-ec84-4295-83ea-4ccede958993"));
        }
    }
}
