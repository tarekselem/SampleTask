using Futura.BusinessOperations.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;

namespace Futura.Services.API.Controllers
{
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        #region Fields
        private readonly ICustomersManager _customersManager;
        #endregion

        #region Contracture
        public CustomerController(ICustomersManager customersManager)
        {
            _customersManager = customersManager;
        }
        #endregion

        #region GET Actions
        [HttpGet]
        public async Task<IHttpActionResult> Get(int pageIndex, int pageSize, string keyword = null)
        {
            var result = await _customersManager.Get(pageIndex, pageSize, keyword);
            return Ok(result);
        }

        [HttpGet(), Route("{id}")]
        public IHttpActionResult Get(string id)
        {
            var result = _customersManager.GetCustomerById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        #endregion

        #region POST Actions
        [HttpPost]
        public IHttpActionResult Post([FromBody] BindingModels.Customer customerBindingModel)
        {
            var result = _customersManager.Add(customerBindingModel);
            return Ok(result);
        }
        #endregion

        #region PUT Actions
        [HttpPut, Route("{id}")]
        public IHttpActionResult Put([FromUri]string id, [FromBody] BindingModels.Customer customerBindingModel)
        {
            if (id != customerBindingModel.Id) return BadRequest("Customer ID is not same.");

            bool result = _customersManager.Update(customerBindingModel);

            if (result) return Ok();
            return NotFound();
        }
        #endregion

        #region DELETE Actions
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(string id)
        {
            bool result = _customersManager.Delete(id);

            if (result) return Ok();
            return NotFound();
        }
        #endregion

    }
}
