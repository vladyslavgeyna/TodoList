using Microsoft.Extensions.Configuration;

namespace TodoList.Service.Utils
{
    public static class ProjectPathGetter
    {
        public static string GetXmlStorageFilePath(IConfiguration configuration)
        {
            string workingDirectory = Environment.CurrentDirectory;
            var xmlStorageFilePath = configuration["XmlStorageFilePath"];
            if (xmlStorageFilePath is null)
            {
                throw new Exception("Xml file path not found.");
            }
            return Path.Combine(workingDirectory, xmlStorageFilePath);
        }
    }
}
