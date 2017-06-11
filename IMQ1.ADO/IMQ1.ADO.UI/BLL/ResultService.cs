using IMQ1.ADO.ORM.Entities;
using System.Collections.Generic;
using System.Linq;
using IMQ1.ADO.UI.BLL.Interface;
using IMQ1.ADO.UI.DAL.Interface;

namespace IMQ1.ADO.UI.BLL
{
    public class ResultService : IResultService
    {
        private IUnitOfWork _uow;
        public ResultService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void AddNewEmployee(Employee employee)
        {
            _uow.Repository<Employee>().AddOrUpdate(employee);
            _uow.SaveChanges();
        }

        public dynamic GetEmployees(string region)
        {
            return _uow
                .Entity<Employee>()
                .Where(employes => employes.Region == region)
                .Select(emp => new { emp.LastName, emp.Title, emp.Region });
        }

        public dynamic GetEmployeesStatistics()
        {
            return _uow
                .Entity<Employee>()
                .GroupBy(employes => employes.Region)
                .Select(group => new
                {
                    Region = group.Key,
                    Count = group.Count()
                })
                .OrderBy(info => info.Region);
        }

        public dynamic GetProducts(string category, string supplier)
        {
            return _uow
                .Entity<Product>()
                .Where(product => product.Category.CategoryName == category && product.Supplier.CompanyName == supplier)
                .Select(product => new { product.ProductName, product.Category.CategoryName, product.Supplier.CompanyName });
        }

        public IEnumerable<Territory> GetTerritories()
        {
            return _uow
                .Entity<Territory>();
        }

        public void SwitchProductCategory(string from, string to)
        {
            var products = _uow.Entity<Product>()
                .Where(product => product.Category.CategoryName == from);

            var result = _uow.Entity<Category>().Where(cat => cat.CategoryName == to)?.FirstOrDefault();

            if (result == null) return;
            {
                var idOfCategory = result?.CategoryID;
                foreach (var product in products)
                {
                    product.CategoryID = idOfCategory;
                }
                _uow.SaveChanges();
            }
        }

        public void AddProducts(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                var resultCategory = _uow.Entity<Category>().Where(cat => cat.CategoryName == product.Category.CategoryName)?.FirstOrDefault() ?? product.Category;
                var resultSupplier = _uow.Entity<Supplier>()
                                         .Where(sup => sup.CompanyName == product.Supplier.CompanyName)
                                         ?.FirstOrDefault() ?? product.Supplier;
                product.Category = resultCategory;
                product.Supplier = resultSupplier;
            }
            _uow.Repository<Product>().AddRange(products);
            _uow.SaveChanges();
        }
    }
}
