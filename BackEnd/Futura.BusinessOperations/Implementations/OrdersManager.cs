using Futura.BusinessOperations.Interfaces;
using Futura.DataAccess.Common;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Futura.BusinessOperations.Implementations
{
    public class OrdersManager : IOrdersManager
    {
        #region Private Fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public OrdersManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Interface Implementation     
        public ViewModels.Order Add(BindingModels.Order orderBindingModel)
        {
            var orderEntity = AutoMapper.Mapper.Map<Entities.Order>(orderBindingModel);
            var addedOrder = _unitOfWork.RepositoryFor<Entities.Order>().Insert(orderEntity);
            _unitOfWork.SaveChanges();

            return AutoMapper.Mapper.Map<ViewModels.Order>(addedOrder);
        }

        public bool Delete(Guid id)
        {
            bool result = _unitOfWork.RepositoryFor<Entities.Order>().Delete(id);
            if (result) _unitOfWork.SaveChanges();

            return result;
        }

        public async Task<ViewModels.OrdersList> Get(int pageIndex, int pageSize, string keyword = null)
        {
            var predicate = BuildSearchFilter(keyword);

            var items = await _unitOfWork.RepositoryFor<Entities.Order>().GetAsync(order => new ViewModels.Order
            {
                Id = order.Id,
                EmployeeId = order.EmployeeId,
                OrderDate = order.OrderDate,
                CustomerName = order.ShipName,
                RequiredDate = order.RequiredDate,
                ShippedDate = order.ShippedDate.Value
            }, predicate, c => c.OrderBy(o => o.Id), null, pageIndex, pageSize);
            var total = _unitOfWork.RepositoryFor<Entities.Order>().Get().Count();

            return new ViewModels.OrdersList { Items = items, Total = total };
        }

        public ViewModels.OrderDetails GetOrderById(Guid id)
        {
            var includedProperties = new List<Expression<Func<Entities.Order, object>>>() { c => c.Customer };
            var orderEntity = _unitOfWork.RepositoryFor<Entities.Order>().Get(filter: c => c.Id == id, includedProperties: includedProperties).FirstOrDefault();

            if (orderEntity == null) return null;

            return AutoMapper.Mapper.Map<ViewModels.OrderDetails>(orderEntity);
        }

        public bool Update(BindingModels.Order orderBindingModel)
        {
            var orderEntity = AutoMapper.Mapper.Map<Entities.Order>(orderBindingModel);
            bool result = _unitOfWork.RepositoryFor<Entities.Order>().Update(orderEntity);

            if (result) _unitOfWork.SaveChanges();

            return result;
        }
        #endregion

        #region Private Methods
        private ExpressionStarter<Entities.Order> BuildSearchFilter(string keyword)
        {
            var predicate = PredicateBuilder.True<Entities.Order>();

            if (!string.IsNullOrEmpty(keyword))
            {
                predicate = predicate.And(s => s.CustomerId.ToLower().Contains(keyword.ToLower()));
                predicate = predicate.And(s => s.ShipAddress.ToLower().Contains(keyword.ToLower()));
                predicate = predicate.And(s => s.ShipCity.ToLower().Contains(keyword.ToLower()));
                predicate = predicate.And(s => s.ShipCountry.ToLower().Contains(keyword.ToLower()));
                predicate = predicate.And(s => s.ShipName.ToLower().Contains(keyword.ToLower()));
                predicate = predicate.And(s => s.ShipPostalCode.ToLower().Contains(keyword.ToLower()));
                predicate = predicate.And(s => s.ShipRegion.ToLower().Contains(keyword.ToLower()));
            }

            return predicate;
        }
        #endregion

    }
}
