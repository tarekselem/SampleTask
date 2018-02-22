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
    public class CustomersManager : ICustomersManager
    {
        #region Private Fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public CustomersManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Interface Implementation

        public async Task<ViewModels.CustomersList> Get(int pageIndex, int pageSize, string keyword)
        {
            var predicate = BuildSearchFilter(keyword);

            var items = await _unitOfWork.RepositoryFor<Entities.Customer>().GetAsync(customer => new ViewModels.Customer
            {
                Id = customer.Id,
                CompanyName = customer.CompanyName,
                ContactName = customer.ContactName,
                ContactTile = customer.ContactTile,
                Phone = customer.Phone,
                Address = customer.Address,
                City = customer.City,
                Country = customer.Country,
                OrdersCount = customer.Orders.Count
            }, predicate, c => c.OrderBy(o => o.Id), null, pageIndex, pageSize);
            var total = _unitOfWork.RepositoryFor<Entities.Customer>().Get().Count();

            return new ViewModels.CustomersList { Items = items, Total = total };
        }

        public ViewModels.CustomerDetails GetCustomerById(Guid id)
        {
            var includedProperties = new List<Expression<Func<Entities.Customer, object>>>() { c => c.Orders };
            var customerEntity = _unitOfWork.RepositoryFor<Entities.Customer>().Get(filter: c => c.Id == id, includedProperties: includedProperties).FirstOrDefault();

            if (customerEntity == null) return null;

            return AutoMapper.Mapper.Map<ViewModels.CustomerDetails>(customerEntity);
        }


        public ViewModels.Customer Add(BindingModels.Customer customerBindingModel)
        {
            if (customerBindingModel == null) throw new ArgumentNullException(nameof(customerBindingModel));

            var customerEntity = AutoMapper.Mapper.Map<Entities.Customer>(customerBindingModel);
            var addedCustomer = _unitOfWork.RepositoryFor<Entities.Customer>().Insert(customerEntity);
            _unitOfWork.SaveChanges();

            return AutoMapper.Mapper.Map<ViewModels.Customer>(addedCustomer);
        }

        public bool Update(BindingModels.Customer customerBindingModel)
        {
            if (customerBindingModel == null) throw new ArgumentNullException(nameof(customerBindingModel));

            var customerEntity = AutoMapper.Mapper.Map<Entities.Customer>(customerBindingModel);
            bool result = _unitOfWork.RepositoryFor<Entities.Customer>().Update(customerEntity);

            if (result) _unitOfWork.SaveChanges();

            return result;
        }

        public bool Delete(Guid id)
        {
            if (Guid.Empty == id) throw new ArgumentNullException(nameof(id));

            bool result = _unitOfWork.RepositoryFor<Entities.Customer>().Delete(id);
            if (result) _unitOfWork.SaveChanges();

            return result;
        }

        #endregion

        #region Private Methods
        private ExpressionStarter<Entities.Customer> BuildSearchFilter(string keyword)
        {
            var predicate = PredicateBuilder.True<Entities.Customer>();

            if (!string.IsNullOrEmpty(keyword))
            {
                predicate = predicate.And(s => s.Address.ToLower().Contains(keyword.ToLower()));
                predicate = predicate.And(s => s.City.ToLower().Contains(keyword.ToLower()));
                predicate = predicate.And(s => s.CompanyName.ToLower().Contains(keyword.ToLower()));
                predicate = predicate.And(s => s.ContactName.ToLower().Contains(keyword.ToLower()));
                predicate = predicate.And(s => s.ContactTile.ToLower().Contains(keyword.ToLower()));
                predicate = predicate.And(s => s.Country.ToLower().Contains(keyword.ToLower()));
                predicate = predicate.And(s => s.Fax.ToLower().Contains(keyword.ToLower()));
                predicate = predicate.And(s => s.Phone.ToLower().Contains(keyword.ToLower()));
            }

            return predicate;
        }
        #endregion
    }
}
