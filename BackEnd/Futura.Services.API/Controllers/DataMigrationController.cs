using Futura.BusinessOperations.Interfaces;
using System.Threading.Tasks;
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
        public IHttpActionResult Import(string id)
        {
            _xmlMigrationManager.ImportXml(id);
            return Ok();
        }
        #endregion
    }
}
