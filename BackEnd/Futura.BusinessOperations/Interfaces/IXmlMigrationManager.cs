

namespace Futura.BusinessOperations.Interfaces
{
    public interface IXmlMigrationManager
    {
        ViewModels.ImportedXmlRecords ImportXml(BindingModels.FileUpload xmlFile);
        object ExportXml();
    }
}
