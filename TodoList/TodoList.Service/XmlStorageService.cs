using Microsoft.Extensions.Configuration;
using TodoList.Service.Utils;

namespace TodoList.Service
{
    public class XmlStorageService
    {
        public readonly string XmlStoragePath;
        public XmlStorageService(IConfiguration configuration)
        {
            XmlStoragePath = ProjectPathGetter.GetXmlStorageFilePath(configuration);
        }
    }
}
