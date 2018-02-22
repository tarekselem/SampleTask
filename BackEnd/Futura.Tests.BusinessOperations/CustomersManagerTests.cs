using Futura.BusinessOperations.Configurations;
using Futura.BusinessOperations.Implementations;
using Futura.BusinessOperations.Interfaces;
using Futura.DataAccess.Common;
using Moq;
using NUnit.Framework;
using System;

namespace Futura.Tests.BusinessOperations
{

    [TestFixture]
    public class CustomersManagerTests
    {
        private ICustomersManager _customersManager;
        private Mock<IUnitOfWork> _unitOfWork;

        [OneTimeSetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _customersManager = new CustomersManager(_unitOfWork.Object);
            MappingConfigurations.Configure();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Add_AddNewCustomer_ReturnsViewModel(bool isNull)
        {
            //Arrange
            _unitOfWork.Setup(x => x.RepositoryFor<Entities.Customer>().Insert(It.IsAny<Entities.Customer>())).Returns<Entities.Customer>(x => x);

            //Act
            ViewModels.Customer actFunc() => _customersManager.Add(isNull ? null : new BindingModels.Customer()
            {
                CustomerId = "GREAL",
                CompanyName = "Great Lakes Food Market",
                ContactName = "Howard Snyder",
                ContactTile = "Marketing Manager",
                Phone = "(503) 555-7555",
                Address = "2732 Baker Blvd.",
                PostalCode = "97403",
                Country = "USA"
            });
          
            //Assert
            if(isNull)
                Assert.That(() => actFunc(),Throws.TypeOf<ArgumentNullException>());
            else
                Assert.IsNotNull(actFunc());
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Update_UpdateCustomer_ReturnsTrue(bool isNull)
        {
            //Arrange
            _unitOfWork.Setup(x => x.RepositoryFor<Entities.Customer>().Update(It.IsAny<Entities.Customer>())).Returns(!isNull);

            //Act
            var result = _customersManager.Update(isNull ? null : new BindingModels.Customer()
            {
                CustomerId = "GREAL",
                CompanyName = "Great Lakes Food Market",
                ContactName = "Howard Snyder",
                ContactTile = "Marketing Manager",
                Phone = "(503) 555-7555",
                Address = "2732 Baker Blvd.",
                PostalCode = "97403",
                Country = "USA"
            });

            //Assert
            Assert.IsTrue(result != isNull);
        }
    }
}
