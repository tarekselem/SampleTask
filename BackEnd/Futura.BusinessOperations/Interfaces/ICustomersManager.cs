using System.Threading.Tasks;

namespace Futura.BusinessOperations.Interfaces
{
    public interface ICustomersManager
    {
        Task<ViewModels.CustomersList> Get(int pageIndex, int pageSize, string keyword = null);
        ViewModels.CustomerDetails GetCustomerById(string id);
        ViewModels.Customer Add(BindingModels.Customer customerBindingModel);
        bool Update(BindingModels.Customer customerBindingModel);
        bool Delete(string id);
    }
}
