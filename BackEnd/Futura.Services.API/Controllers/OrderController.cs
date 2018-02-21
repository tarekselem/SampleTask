using Futura.BusinessOperations.Interfaces;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Futura.Services.API.Controllers
{
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        #region Fields
        private readonly IOrdersManager _ordersManager;
        #endregion

        #region Contracture
        public OrderController(IOrdersManager ordersManager)
        {
            _ordersManager = ordersManager;
        }
        #endregion

        #region GET Actions
        [HttpGet]
        public async Task<IHttpActionResult> Get(int pageIndex, int pageSize, string keyword = null)
        {
            var result = await _ordersManager.Get(pageIndex, pageSize, keyword);
            return Ok(result);
        }

        [HttpGet(), Route("{id}")]
        public IHttpActionResult Get(Guid id)
        {
            var result = _ordersManager.GetOrderById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        #endregion

        #region POST Actions
        [HttpPost]
        public IHttpActionResult Post([FromBody] BindingModels.Order orderBindingModel)
        {
            var result = _ordersManager.Add(orderBindingModel);
            return Ok(result);
        }
        #endregion

        #region PUT Actions
        [HttpPut, Route("{id}")]
        public IHttpActionResult Put([FromUri]Guid id, [FromBody] BindingModels.Order orderBindingModel)
        {
            if (id != orderBindingModel.Id) return BadRequest("Order ID is not same.");

            bool result = _ordersManager.Update(orderBindingModel);

            if (result) return Ok();
            return NotFound();
        }
        #endregion

        #region DELETE Actions
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            bool result = _ordersManager.Delete(id);

            if (result) return Ok();
            return NotFound();
        }
        #endregion
    }
}
