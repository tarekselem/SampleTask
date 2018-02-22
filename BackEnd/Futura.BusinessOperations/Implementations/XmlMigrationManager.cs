using AutoMapper;
using Futura.BusinessOperations.Interfaces;
using Futura.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Futura.BusinessOperations.Implementations
{
    public class XmlMigrationManager : IXmlMigrationManager
    {
        #region Private Fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public XmlMigrationManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Interface Implementation
        public object ExportXml()
        {
            //Return XML object
            return null;
        }

        public void ImportXml(string xmlData)
        {
            XmlModels.Customers customers;
            XmlModels.Orders orders;

            //using (StreamReader reader = new StreamReader(xmlFilePath))
            //{
            //    XmlSerializer serializer = new XmlSerializer(typeof(XmlModels.Root));
            //    var deserializedXmlRoot = (XmlModels.Root)serializer.Deserialize(reader);
            //    customers = deserializedXmlRoot.Customers.Customer;
            //    orders = deserializedXmlRoot.Orders.Order;

            //    //check if has data
            //    if (customers == null && orders == null) throw new Exception("No Customers nor Orders XML data found.");
            //}

            using (TextReader reader = new StringReader(xmlData))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(XmlModels.Root));
                var deserializedXmlRoot = (XmlModels.Root)serializer.Deserialize(reader);
                customers = deserializedXmlRoot.Customers;
                orders = deserializedXmlRoot.Orders;

                //check if has data
                if (customers == null && orders == null) throw new Exception("No Customers nor Orders XML data found.");
            }

            InsertXmlCustomers(customers.Customer);
            InsertXmlOrders(orders.Order);
        }
        #endregion

        #region Private Methods
        private bool CustomerIsExist(string customerId)
        {
            return _unitOfWork.RepositoryFor<Entities.Customer>().Get(c => c.Id.ToLower() == customerId.ToLower()).Any();
        }

        private bool OrderIsExist(string customerId, DateTime orderDate)
        {
            return _unitOfWork.RepositoryFor<Entities.Order>().Get(o => o.CustomerId.ToLower() == customerId.ToLower() && o.OrderDate == orderDate).Any();
        }

        private bool CustomerFound(string customerId)
        {
            return _unitOfWork.RepositoryFor<Entities.Customer>().Get(c => c.Id.ToLower() == customerId.ToLower()).Any();
        }

        public List<Entities.Customer> InsertXmlCustomers(List<XmlModels.Customer> customers)
        {
            var customersToInsert = new List<Entities.Customer>();

            foreach (var customer in customers)
            {
                //Check if customer exist
                if (CustomerIsExist(customer.CustomerID)) continue;

                //insert customer
                var customerEntity = Mapper.Map<Entities.Customer>(customer);
                customersToInsert.Add(customerEntity);
            }
            _unitOfWork.RepositoryFor<Entities.Customer>().BulkInsert(customersToInsert);
            _unitOfWork.SaveChanges();

            return customersToInsert;
        }

        private List<Entities.Order> InsertXmlOrders(List<XmlModels.Order> orders)
        {
            var ordersToInsert = new List<Entities.Order>();

            foreach (var order in orders)
            {
                //Check if order exists
                if (OrderIsExist(order.CustomerID, order.OrderDate)) continue;

                //Check if order customer not found
                if (!CustomerFound(order.CustomerID)) continue;

                //insert order
                var orderEntity = Mapper.Map<Entities.Order>(order);
                ordersToInsert.Add(orderEntity);
            }
            _unitOfWork.RepositoryFor<Entities.Order>().BulkInsert(ordersToInsert);
            _unitOfWork.SaveChanges();

            return ordersToInsert;
        }

        #endregion

    }
}
