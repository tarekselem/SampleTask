using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futura.BusinessOperations.Helpers
{
    public static class XmlMigrationHelper
    {
        //public static List<Entities.Customer> InsertXmlCustomers(List<XmlModels.Customer> customers)
        //{
        //    var customersToInsert = new List<Entities.Customer>();

        //    foreach (var customer in customers)
        //    {
        //        //Check if customer exist
        //        if (CustomerIsExist(customer.CustomerID)) continue;

        //        //insert customer
        //        var customerEntity = Mapper.Map<Entities.Customer>(customer);
        //        customersToInsert.Add(customerEntity);
        //    }
        //    _unitOfWork.RepositoryFor<Entities.Customer>().BulkInsert(customersToInsert);
        //    _unitOfWork.SaveChanges();

        //    return customersToInsert;
        //}
    }
}
