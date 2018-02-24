using Futura.BusinessOperations.Interfaces;
using System.Web.Http;

namespace Futura.Services.API.Controllers
{
    public class DataMigrationController : ApiController
    {
        #region Fields
        private readonly IXmlMigrationManager _xmlMigrationManager;
        #endregion

        #region Contracture
        public DataMigrationController(IXmlMigrationManager xmlMigrationManager)
        {
            _xmlMigrationManager = xmlMigrationManager;
        }
        #endregion

        #region GET Actions
        [HttpGet(), Route("XML")]
        public IHttpActionResult Export()
        {
            _xmlMigrationManager.ExportXml();
            return Ok();
        }
        #endregion

        #region POST Actions
        [HttpPost(), Route("XML")]
        public IHttpActionResult Import([FromBody] BindingModels.FileUpload xmlFile)
        {
            var result = _xmlMigrationManager.ImportXml(xmlFile);
            return Ok($"Total inserted Customers: {result.CustomersCount} , Total inserted Orders: {result.OrdersCount}");
        }
        #endregion
    }
}
