using Futura.BusinessOperations.Implementations;
using Futura.BusinessOperations.Interfaces;
using Futura.DataAccess.Common;
using Moq;
using NUnit.Framework;
using System;

namespace Futura.Tests.BusinessOperations
{
    [TestFixture]
    public class XmlMigrationManagerTests
    {
        private IXmlMigrationManager _xmlMigrationManager;
        private Mock<IUnitOfWork> _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _xmlMigrationManager = new XmlMigrationManager(_unitOfWork.Object);
        }

        [Ignore("")]
        [Test]
        [Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(Exception))]
        public void ImportXml_InsertEmptyXmlNodes_ThrowException()
        {
            //Arrange
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?><Root></Root>";

            try
            {
                //Act
                //_xmlMigrationManager.ImportXml(xml);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                //Assert
                Assert.IsTrue(ex.Message.Contains("No Customers nor Orders XML data found."));
            }
        }
        

        //[TestCase(true,true)]
        //[TestCase(false,true)]
        //[TestCase(true,false)]
        //[TestCase(false, false, ExpectedResult = typeof(Exception))]
        //public void ImportXml_PassingEmptyXmlNodes_ThrowException(bool hasCustomers, bool hasOrders)
        //{
        //    //Arrange
        //    var xmlSource = string.Empty;
        //    if (hasCustomers && hasOrders)
        //        xmlSource = "";
        //    else if (hasCustomers && !hasOrders)
        //        xmlSource = "";
        //    else if (!hasCustomers && hasOrders)
        //        xmlSource = "";
        //    else
        //        xmlSource = @"<?xml version=""1.0"" encoding=""utf-8""?><Root></Root>";
        //    //Act
        //    _xmlMigrationManager.ImportXml(xmlSource);

        //    //load from Database // Customers & Orders

        //    //Assert
        //    if (hasOrders)
        //        //Assert.IsTrue(orders.Any())
        //        throw new NotImplementedException();
        //    if(hasCustomers)
        //        //Assert.IsTrue(customers.Any())
        //        throw new NotImplementedException();

        //    Assert.IsTrue(ex.Message.Contains("No Customers nor Orders XML data found."));
        //}
    }
}
