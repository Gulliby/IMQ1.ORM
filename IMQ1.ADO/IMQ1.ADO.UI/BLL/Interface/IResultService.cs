using IMQ1.ADO.ORM.Entities;
using System.Collections.Generic;

namespace IMQ1.ADO.UI.BLL.Interface
{
    public interface IResultService
    {
        dynamic GetProducts(string category, string supplier);

        dynamic GetEmployees(string region);

        dynamic GetEmployeesStatistics();

        void AddNewEmployee(Employee employee);

        IEnumerable<Territory> GetTerritories();

        void SwitchProductCategory(string from, string to);

        void AddProducts(IEnumerable<Product> products);
    }
}
