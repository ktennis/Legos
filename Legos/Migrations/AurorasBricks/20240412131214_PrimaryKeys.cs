using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Legos.Migrations.AurorasBricks
{
    /// <inheritdoc />
    public partial class PrimaryKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    customer_ID = table.Column<int>(type: "int", nullable: true),
                    firstname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    lastname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    birthdate = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    countryofresidence = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    gender = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    age = table.Column<int>(type: "int", nullable: true),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "line_items",
                columns: table => new
                {
                    transaction_ID = table.Column<int>(type: "int", nullable: true),
                    productID = table.Column<int>(type: "int", nullable: true),
                    qty = table.Column<int>(type: "int", nullable: true),
                    rating = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    transaction_ID = table.Column<int>(type: "int", nullable: true),
                    customerID = table.Column<int>(type: "int", nullable: true),
                    date = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    dayofweek = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    time = table.Column<int>(type: "int", nullable: true),
                    entrymode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    amount = table.Column<int>(type: "int", nullable: true),
                    typeoftransaction = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    countryoftransaction = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    shippingaddress = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    bank = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    typeofcard = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    fraud = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    product_ID = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: true),
                    year = table.Column<int>(type: "int", nullable: true),
                    numparts = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<int>(type: "int", nullable: true),
                    imglink = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: true),
                    primarycolor = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    secondarycolor = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "varchar(4096)", unicode: false, maxLength: 4096, nullable: true),
                    category = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    rec1 = table.Column<int>(type: "int", nullable: true),
                    rec2 = table.Column<int>(type: "int", nullable: true),
                    rec3 = table.Column<int>(type: "int", nullable: true),
                    rec4 = table.Column<int>(type: "int", nullable: true),
                    rec5 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "line_items");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
