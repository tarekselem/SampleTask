using Futura.BusinessOperations.Implementations;
using Futura.BusinessOperations.Interfaces;
using Futura.DataAccess.Common;
using Moq;
using NUnit.Framework;
using System;

namespace Futura.Tests.BusinessOperations
{
    [TestFixture]
    public class OrdersManagerTests
    {
        private IOrdersManager _ordersManager;
        private Mock<IUnitOfWork> _unitOfWork;

        [OneTimeSetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _ordersManager = new OrdersManager(_unitOfWork.Object);
        }

        [TestCase(true)] // passing null
        [TestCase(false)] // passing binding model
        public void Add_WhenCalled_ReturnsViewModelOfAddedOrder(bool bindingModelIsNull)
        {
            //Arrange
            _unitOfWork.Setup(x => x.RepositoryFor<Entities.Order>().Insert(It.IsAny<Entities.Order>())).Returns<Entities.Order>(x => x);

            //Act
            ViewModels.Order func() => _ordersManager.Add(bindingModelIsNull ? null : new BindingModels.Order()
            {
                Id = Guid.NewGuid(),
                EmployeeId = 1,
                OrderDate = DateTime.Now,
                RequiredDate = DateTime.Now,
                ShippedDate = DateTime.Now,
                ShipVia = 4,
                Freight = 3.5M,
                ShipName = "Great Lakes Food Market",
                ShipAddress = "2732 Baker Blvd.",
                ShipCity = "Eugene",
                ShipRegion = "OR",
                ShipPostalCode = "97403",
                ShipCountry = "USA"
            });

            //Assert
            if (bindingModelIsNull)
                Assert.That(() => func(), Throws.TypeOf<ArgumentNullException>());
            else
                Assert.IsNotNull(func());
        }


        [TestCase(true)] // passing null
        [TestCase(false)] // passing binding model
        public void Update_WhenCalled_ReturnsBoolean(bool bindingModelIsNull)
        {
            //Arrange
            _unitOfWork.Setup(x => x.RepositoryFor<Entities.Order>().Update(It.IsAny<Entities.Order>())).Returns(!bindingModelIsNull);

            //Act
            bool func() => _ordersManager.Update(bindingModelIsNull ? null : new BindingModels.Order()
            {
                Id = Guid.NewGuid(),
                EmployeeId = 1,
                OrderDate = DateTime.Now,
                RequiredDate = DateTime.Now,
                ShippedDate = DateTime.Now,
                ShipVia = 4,
                Freight = 3.5M,
                ShipName = "Great Lakes Food Market",
                ShipAddress = "2732 Baker Blvd.",
                ShipCity = "Eugene",
                ShipRegion = "OR",
                ShipPostalCode = "97403",
                ShipCountry = "USA"
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
            _unitOfWork.Setup(x => x.RepositoryFor<Entities.Order>().Delete(Guid.NewGuid())).Returns(!isEmptyGuid);

            //Act
            bool func() => _ordersManager.Delete(isEmptyGuid ? Guid.Empty : Guid.NewGuid());

            //Assert
            if (isEmptyGuid)
                Assert.That(() => func(), Throws.TypeOf<ArgumentNullException>());
            else
                Assert.IsFalse(func());
        }


    }

}
