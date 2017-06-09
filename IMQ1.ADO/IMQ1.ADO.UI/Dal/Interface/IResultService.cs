using IMQ1.ADO.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMQ1.ADO.UI.Dal.Interface
{
    public interface IResultService
    {
        //Список продуктов с категорией и поставщиком. 
        dynamic GetProducts(string category, string supplier);

        //Список сотрудников с указанием региона, за который они отвечают.
        dynamic GetEmployees(string region);

        //Статистики по регионам: количества сотрудников по регионам.
        dynamic GetEmployeesStatistics();
        
        //Добавить нового сотрудника, и указать ему список территорий, за которые он несет ответственность. 
        void AddNewEmployee(Employee employee);

        //Перенести продукты из одной категории в другую 
        //Добавить список продуктов со своими поставщиками и продуктами (массовое занесение), 
        //при этом если поставщик или категория с таким названием есть, то использовать их – иначе создать новые. 
        //Замена продукта на аналогичный: во всех еще неисполненных заказах (считать таковыми заказы, у которых ShippedDate = NULL) заменить один продукт на другой.
    }
}
