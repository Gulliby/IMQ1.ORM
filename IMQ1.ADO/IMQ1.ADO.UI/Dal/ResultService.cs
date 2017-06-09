using IMQ1.ADO.ORM.Entities;
using IMQ1.ADO.UI.Bll.Interface;
using IMQ1.ADO.UI.Dal.Interface;
using System.Linq;

namespace IMQ1.ADO.UI.Dal
{
    public class ResultService : IResultService
    {
        private IUnitOfWork _uow;
        public ResultService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public dynamic GetEmployees(string region)
        {
            return _uow
                .Entity<Employee>()
                .Where(employes => employes.Region.Equals(region));
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
                .Where(product => product.Category.CategoryName == category && product.Supplier.CompanyName == supplier);
        }
    }
}
