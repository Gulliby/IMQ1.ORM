using IMQ1.ADO.ORM;
using IMQ1.ADO.UI.Bll;
using IMQ1.ADO.UI.Bll.Interface;
using IMQ1.ADO.UI.Dal;
using IMQ1.ADO.UI.Dal.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IMQ1.ADO.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\",string.Empty);
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            
            IUnitOfWork uow = new UnitOfWork<NorthwindContext>(new NorthwindContext(ConfigurationManager.ConnectionStrings["NorthwindContext"].ConnectionString));
            IResultService resultService = new ResultService(uow);

            Console.WriteLine(ParseJson(resultService.GetEmployeesStatistics()));
            Console.ReadKey();
        }

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
