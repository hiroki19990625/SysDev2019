using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LogAdapter;
using ObjectDatabase;
using SysDev2019.DataModels;
using Database = ObjectDatabase.ObjectDatabase;

namespace SysDev2019.DatabaseConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string cmd = Console.ReadLine();

            Database database = new Database("../../../SysDev2019/SysDev2019.accdb", logCallback: OnLog);
            DataTable<Product> productTable = new DataTable<Product>("商品");
            DataTable<Manufacturer> manufacturerTable = new DataTable<Manufacturer>("メーカー");
            DataTable<Department> departmentTable = new DataTable<Department>("部署");
            DataTable<Employee> employeeTable = new DataTable<Employee>("社員");
            DataTable<Order> orderTable = new DataTable<Order>("受注");
            DataTable<Ordering> orderingTable = new DataTable<Ordering>("発注");
            DataTable<Stock> stockTable = new DataTable<Stock>("在庫");
            database.AddTable(manufacturerTable);
            database.AddTable(productTable);
            database.AddTable(departmentTable);
            database.AddTable(employeeTable);
            database.AddTable(orderingTable);
            database.AddTable(orderTable);
            database.AddTable(stockTable);

            productTable.Union(manufacturerTable);
            employeeTable.Union(departmentTable);
            orderTable.Union(employeeTable, "EmployeeId");
            orderTable.Union(productTable, "ProductId");
            orderingTable.Union(productTable, "ProductId");
            orderingTable.Union(employeeTable, "EmployeeId");
            stockTable.Union(productTable);

            database.GenerateCode("SysDev2019");

            if (cmd == "seed")
            {
                // All Delete
                stockTable.Delete(p => true);
                productTable.Delete(p => true);
                manufacturerTable.Delete(p => true);

                List<Manufacturer> manufacturers = new List<Manufacturer>();
                for (int i = 0; i < 100; i++)
                {
                    manufacturers.Add(new Manufacturer
                    {
                        ManufacturerId = i.ToString(),
                        ManufacturerName = $"化粧品メーカー_{i}"
                    });
                }

                manufacturerTable.Insert(manufacturers.ToArray());

                Random r = new Random();
                List<Product> products = new List<Product>();
                for (int i = 0; i < 12000; i++)
                {
                    var price = r.Next(100, 1000) * 10;

                    products.Add(new Product
                    {
                        ProductId = i.ToString(),
                        ManufacturerId = r.Next(0, 100).ToString(),
                        ProductName = price >= 6000 ? $"高級化粧品_{i}" : $"化粧品_{i}",
                        UnitPrice = price
                    });
                }

                productTable.Insert(products.ToArray());

                stockTable.Insert(new Stock
                {
                    StockId = Guid.NewGuid().ToString(),
                    ProductId = "0",
                    StockQuantity = 1000,
                    ReorderPoint = 400,
                    OrderQuantity = 1000
                });
                stockTable.Insert(new Stock
                {
                    StockId = Guid.NewGuid().ToString(),
                    ProductId = "1",
                    StockQuantity = 600,
                    ReorderPoint = 500,
                    OrderQuantity = 1000
                });
                stockTable.Insert(new Stock
                {
                    StockId = Guid.NewGuid().ToString(),
                    ProductId = "2",
                    StockQuantity = 100,
                    ReorderPoint = 10,
                    OrderQuantity = 100
                });
                stockTable.Insert(new Stock
                {
                    StockId = Guid.NewGuid().ToString(),
                    ProductId = "3",
                    StockQuantity = 3000,
                    ReorderPoint = 400,
                    OrderQuantity = 1000
                });
            }
            else if (cmd == "user")
            {
                Console.Write("ID >> ");
                string id = Console.ReadLine();
                Console.Write("Name >> ");
                string name = Console.ReadLine();
                Console.Write("Pass >> ");
                string pass = ToSha256(Console.ReadLine());
                Console.Write("DeptId >> ");
                string dept = Console.ReadLine();
                Console.WriteLine("PasswordHash: " + pass);

                Employee employee = new Employee {EmployeeId = id, Name = name, Password = pass, DepartmentId = dept};
                employeeTable.Insert(employee);
            }
        }

        static string ToSha256(string password)
        {
            SHA256 sha256 = SHA256.Create();
            sha256.Initialize();

            Encoding encoding = Encoding.UTF8;
            byte[] hash = sha256.ComputeHash(encoding.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        static void OnLog(ILogMessage logMessage)
        {
            Console.WriteLine($"[{logMessage.LogLevel}] {logMessage.Data}");
        }
    }
}