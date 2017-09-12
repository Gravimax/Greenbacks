using Greenbacks.ImportData.Models;

namespace Greenbacks.ImportData
{
    public interface IImport
    {
        ImportModel ProcessImport(string[] fileData);   
    }
}
