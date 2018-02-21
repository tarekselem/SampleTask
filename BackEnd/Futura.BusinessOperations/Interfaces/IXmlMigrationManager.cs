

namespace Futura.BusinessOperations.Interfaces
{
    public interface IXmlMigrationManager
    {
        void ImportXml(string xmlFilePath);
        object ExportXml();
    }
}
