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
        }

   

        [TestCase(true)] // passing null
        [TestCase(false)] // passing binding model
        public void Add_WhenCalled_ReturnsViewModelOfAddedCustomer(bool bindingModelIsNull)
        {
            //Arrange
            _unitOfWork.Setup(x => x.RepositoryFor<Entities.Customer>().Insert(It.IsAny<Entities.Customer>())).Returns<Entities.Customer>(x => x);

            //Act
            ViewModels.Customer func() => _customersManager.Add(bindingModelIsNull ? null : new BindingModels.Customer()
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
            if(bindingModelIsNull)
                Assert.That(() => func(),Throws.TypeOf<ArgumentNullException>());
            else
                Assert.IsNotNull(func());
        }


        [TestCase(true)] // passing null
        [TestCase(false)] // passing binding model
        public void Update_WhenCalled_ReturnsBoolean(bool bindingModelIsNull)
        {
            //Arrange
            _unitOfWork.Setup(x => x.RepositoryFor<Entities.Customer>().Update(It.IsAny<Entities.Customer>())).Returns(!bindingModelIsNull);

            //Act
            bool func() => _customersManager.Update(bindingModelIsNull ? null : new BindingModels.Customer()
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
            if (bindingModelIsNull)
                Assert.That(() => func(), Throws.TypeOf<ArgumentNullException>());
            else
                Assert.IsTrue(func());
        }


        [TestCase(true)] // passing empty id
        [TestCase(false)] // passing customer id
        public void Delete_WhenCalled_ReturnsBoolean(bool isEmptyGuid)
        {
            //Arrange
            _unitOfWork.Setup(x => x.RepositoryFor<Entities.Customer>().Delete(Guid.NewGuid())).Returns(!isEmptyGuid);

            //Act
            bool func() => _customersManager.Delete(isEmptyGuid ? Guid.Empty : Guid.NewGuid());

            //Assert
            if (isEmptyGuid)
                Assert.That(() => func(), Throws.TypeOf<ArgumentNullException>());
            else
                Assert.IsFalse(func());
        }


    }
}
