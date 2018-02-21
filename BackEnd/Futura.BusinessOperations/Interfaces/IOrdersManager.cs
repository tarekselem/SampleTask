using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futura.BusinessOperations.Interfaces
{
    public interface IOrdersManager
    {
        Task<ViewModels.OrdersList> Get(int pageIndex, int pageSize, string keyword = null);
        ViewModels.OrderDetails GetOrderById(Guid id);
        ViewModels.Order Add(BindingModels.Order orderBindingModel);
        bool Update(BindingModels.Order orderBindingModel);
        bool Delete(Guid id);
    }
}
