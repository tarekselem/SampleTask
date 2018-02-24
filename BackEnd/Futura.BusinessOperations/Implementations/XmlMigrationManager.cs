using AutoMapper;
using Futura.BusinessOperations.Interfaces;
using Futura.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
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

        public ViewModels.ImportedXmlRecords ImportXml(BindingModels.FileUpload xmlFile)
        {
            if (xmlFile == null) throw new ArgumentNullException(nameof(xmlFile));

            if (xmlFile.FileBase64 == string.Empty) throw new Exception("Cannot accept an empty file.");

            XmlModels.Customers customers;
            XmlModels.Orders orders;
            byte[] toDecodeByte = Convert.FromBase64String(xmlFile.FileBase64);

            using (var stream = new MemoryStream(toDecodeByte))
            {
                using (XmlReader reader = XmlReader.Create(stream))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(XmlModels.Root));
                    var deserializedXmlRoot = (XmlModels.Root)serializer.Deserialize(reader);
                    customers = deserializedXmlRoot.Customers;
                    orders = deserializedXmlRoot.Orders;

                    //check if has data
                    if (customers == null && orders == null) throw new Exception("No Customers nor Orders XML data found.");
                }
            }

            //using (StreamReader reader = new StreamReader("Customers-Orders.xml"))
            //{
            //    XmlSerializer serializer = new XmlSerializer(typeof(XmlModels.Root));
            //    var deserializedXmlRoot = (XmlModels.Root)serializer.Deserialize(reader);
            //    customers = deserializedXmlRoot.Customers;
            //    orders = deserializedXmlRoot.Orders;

            //    //check if has data
            //    if (customers == null && orders == null) throw new Exception("No Customers nor Orders XML data found.");
            //}

            //using (TextReader reader = new StringReader(xmlData))
            //{
            //    XmlSerializer serializer = new XmlSerializer(typeof(XmlModels.Root));
            //    var deserializedXmlRoot = (XmlModels.Root)serializer.Deserialize(reader);
            //    customers = deserializedXmlRoot.Customers;
            //    orders = deserializedXmlRoot.Orders;

            //    //check if has data
            //    if (customers == null && orders == null) throw new Exception("No Customers nor Orders XML data found.");
            //}

            var insertedCustomers = InsertXmlCustomers(customers.Customer);
            var insertedOrders = InsertXmlOrders(orders.Order);

            return new ViewModels.ImportedXmlRecords { CustomersCount = insertedCustomers, OrdersCount = insertedOrders };
        }
        #endregion

        #region Private Methods
        private bool CustomerIsExist(string customerId)
        {
            return _unitOfWork.RepositoryFor<Entities.Customer>().Get(c => c.CustomerId.ToLower() == customerId.ToLower()).Any();
        }

        private bool OrderIsExist(string customerId, DateTime orderDate)
        {
            return _unitOfWork.RepositoryFor<Entities.Order>().Get(o => o.Customer.CustomerId.ToLower() == customerId.ToLower() && o.OrderDate == orderDate).Any();
        }

        private bool IsCustomerFound(string customerId)
        {
            return _unitOfWork.RepositoryFor<Entities.Customer>().Get(c => c.CustomerId.ToLower() == customerId.ToLower()).Any();
        }

        private Guid GetCustomerGuid(string customerId)
        {
            return _unitOfWork.RepositoryFor<Entities.Customer>().Get(filter: filter => filter.CustomerId.ToLower() == customerId.ToLower()).Select(selector => selector.Id).FirstOrDefault();
        }

        public int InsertXmlCustomers(List<XmlModels.Customer> customers)
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
            if (customersToInsert.Count > 0) _unitOfWork.SaveChanges();

            return customersToInsert.Count;
        }

        private int InsertXmlOrders(List<XmlModels.Order> orders)
        {
            var ordersToInsert = new List<Entities.Order>();

            foreach (var order in orders)
            {
                //Check if order exists
                if (OrderIsExist(order.CustomerID, order.OrderDate)) continue;

                //Check if order customer not found
                if (!IsCustomerFound(order.CustomerID)) continue;

                //GetHashCode customerGuid
                order.CustomerGuid = GetCustomerGuid(order.CustomerID);
                //insert order
                var orderEntity = Mapper.Map<Entities.Order>(order);
                ordersToInsert.Add(orderEntity);
            }

            _unitOfWork.RepositoryFor<Entities.Order>().BulkInsert(ordersToInsert);
            if (ordersToInsert.Count > 0) _unitOfWork.SaveChanges();

            return ordersToInsert.Count;
        }



        #endregion

    }
}
