using TodoList.Utils;

namespace TodoList.Services
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
