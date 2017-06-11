using IMQ1.ADO.ORM;
using IMQ1.ADO.ORM.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using IMQ1.ADO.UI.BLL;
using IMQ1.ADO.UI.BLL.Interface;
using IMQ1.ADO.UI.DAL;
using IMQ1.ADO.UI.DAL.Interface;

namespace IMQ1.ADO.UI
{
    class Program
    {
        private static IResultService _resultService;

        static void Main(string[] args)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\", string.Empty);
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            
            IUnitOfWork uow = new UnitOfWork<NorthwindContext>(new NorthwindContext(ConfigurationManager.ConnectionStrings["NorthwindContext"].ConnectionString));
            _resultService = new ResultService(uow);

            Action task;
            Console.Write("Choose the task: ");
            var switcher = Console.ReadKey().KeyChar;
            switch (switcher)
            {
                case '1':
                    task = Task1;
                    break;
                case '2':
                    task = Task2;
                    break;
                case '3':
                    task = Task3;
                    break;
                case '4':
                    task = Task4;
                    break;
                case '5':
                    task = Task5;
                    break;
                case '6':
                    task = Task6;
                    break;
                default:
                    Console.WriteLine("Finish!");
                    return;    
            }
            Console.WriteLine(" Start!");
            Console.WriteLine($"Task{switcher} was executed!");
            task();
            Console.ReadKey();
        }

        //Список продуктов с категорией и поставщиком.
        //Category: Condiments Supplier: New Orleans Cajun Delights
        static void Task1()
        {
            Console.WriteLine(ParseJson(_resultService.GetProducts("Condiments", "New Orleans Cajun Delights")));
        }

        //Статистики по регионам: количества сотрудников по регионам.
        static void Task2()
        {
            Console.WriteLine(ParseJson(_resultService.GetEmployeesStatistics()));
        }

        //Список сотрудников с указанием региона, за который они отвечают.
        static void Task3()
        {
            Console.WriteLine(ParseJson(_resultService.GetEmployees("WA")));
        }

        //Добавить нового сотрудника, и указать ему список территорий, за которые он несет ответственность. 
        static void Task4()
        {
            var randName = new Random(int.MaxValue);
            var randNumber = new Random(5);
            var emp = new Employee
            {
                LastName = randName.Next().ToString(),
                FirstName = randName.Next().ToString(),
                Territories = _resultService.GetTerritories().Take(randNumber.Next() + 1).ToList()
            };
            Console.WriteLine(ParseJson(emp));
            _resultService.AddNewEmployee(emp);
        }

        //Перенести продукты из одной категории в другую
        static void Task5()
        {
            _resultService.SwitchProductCategory("Condiments", "Beverages");    
        }

        //Добавить список продуктов со своими поставщиками и каьегориями (массовое занесение), 
        //при этом если поставщик или категория с таким названием есть, то использовать их – иначе создать новые.
        static void Task6()
        {
            var randName = new Random(int.MaxValue);
            var products = new List<Product>()
            {
                new Product
                {
                    ProductName = randName.Next().ToString(),
                    Category = new Category
                    {
                        CategoryName = randName.Next().ToString()
                    },
                    Supplier = new Supplier
                    {
                        CompanyName = randName.Next().ToString()
                    }
                },
                new Product
                {
                    ProductName = randName.Next().ToString(),
                    Category = new Category
                    {
                        CategoryName = randName.Next().ToString()
                    },
                    Supplier = new Supplier
                    {
                        CompanyName = randName.Next().ToString()
                    }
                }
            };
            _resultService.AddProducts(products);
        }

        //Замена продукта на аналогичный: во всех еще неисполненных заказах (считать таковыми заказы, у которых ShippedDate = NULL) 
        //заменить один продукт на аналогичный.
        //ЧТО ЭТО ЗНАЧИТ?!

        public static string ParseJson(dynamic content)
        {
            try
            {
                return JObject.FromObject(content).ToString(Newtonsoft.Json.Formatting.Indented);
            }
            catch
            {
                try
                {
                    return JArray.FromObject(content).ToString(Newtonsoft.Json.Formatting.Indented);
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
    }
}
