using Microsoft.Extensions.Configuration;

namespace TodoList.Service.Utils
{
    public static class ProjectPathGetter
    {
        public static string GetXmlStorageFilePath(IConfiguration configuration)
        {
            string? workingDirectory = (Directory.GetParent(Environment.CurrentDirectory)?.FullName) 
                ?? throw new Exception("Unable to get working directory path.");

            var xmlStorageFilePath = configuration["XmlStorageFilePath"] 
                ?? throw new Exception("Xml file path not found.");

            return Path.Combine(workingDirectory, xmlStorageFilePath);
        }
    }
}
