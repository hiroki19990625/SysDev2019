using System;
using System.Collections.Generic;
using System.Linq;
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

            if (cmd == "seed")
            {
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
                orderTable.Union(productTable);
                orderTable.Union(employeeTable);
                orderingTable.Union(productTable);
                orderingTable.Union(employeeTable);
                stockTable.Union(productTable);

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
        }

        static void OnLog(ILogMessage logMessage)
        {
            Console.WriteLine($"[{logMessage.LogLevel}] {logMessage.Data}");
        }
    }
}