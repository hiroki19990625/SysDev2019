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

            if (cmd == "seed")
            {
                // All Delete
                productTable.Delete(p => true);
                manufacturerTable.Delete(p => true);

                for (int i = 0; i < 200; i++)
                {
                    manufacturerTable.Insert(new Manufacturer
                    {
                        ManufacturerId = i.ToString(),
                        ManufacturerName = Guid.NewGuid().ToString()
                    });
                }

                Random r = new Random();

                for (int i = 0; i < 20000; i++)
                {
                    productTable.Insert(new Product
                    {
                        ProductId = i.ToString(),
                        ManufacturerId = r.Next(0, 200).ToString(),
                        ProductName = Guid.NewGuid().ToString(),
                        UnitPrice = r.Next(800, 20000)
                    });
                }
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