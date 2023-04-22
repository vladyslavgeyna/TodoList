using System.Xml.Linq;
using TodoList.Models;
using TodoList.Services;
using Task = System.Threading.Tasks.Task;

namespace TodoList.Repository
{
    public class CategoryXmlRepository : ICategoryRepository
    {
        private XDocument _document;
        private readonly XmlStorageService _xmlStorageService;
        private readonly string _path;
        //private readonly ITaskRepository _taskRepository;
        public CategoryXmlRepository(XmlStorageService xmlStorageService/*, ITaskRepository taskRepository*/)
        {
            //_taskRepository = taskRepository;
            _xmlStorageService = xmlStorageService;
            string filePath = _xmlStorageService.XmlStoragePath;
            _document = XDocument.Load(filePath);
            _path = filePath;
        }
        private async Task<bool> IsCategoryByIdExist(int id)
        {
            return (await GetByIdAsync(id)) is not null;
        }
        public async Task AddAsync(Category category)
        {
            int newCategoryId;
            Random random = new();
            do
            {
                newCategoryId = random.Next(1, int.MaxValue);
            } while (await IsCategoryByIdExist(newCategoryId));
            XElement categoryElement = new XElement("Category",
               new XElement("Id", newCategoryId),
               new XElement("Name", category.Name)
            );
            _document?.Root?.Element("Categories")?.Add(categoryElement);
            await Task.Run(() => _document?.Save(_path));
        }

        public async Task DeleteByIdAsync(int id)
        {
            _document = XDocument.Load(_path);
            XElement? categoryElement = _document?.Root
                ?.Element("Categories")
                ?.Elements("Category")
                .Where(t => (int)t.Element("Id") == id)
                .FirstOrDefault();
            if (categoryElement is not null)
            {
                categoryElement.Remove();
                await Task.Run(() => _document?.Save(_path));
            }
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var categories = await Task.Run(() =>
               _document.Root
                   .Element("Categories")
                   .Elements("Category")
                   .Select(t => new Models.Category
                   {
                       Id = (int)t.Element("Id"),
                       Name = (string)t.Element("Name"),
                   })
            );
            return categories;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await Task.Run(() =>
                _document.Root
                    .Element("Categories")
                    .Elements("Category")
                    .Where(t => (int)t.Element("Id") == id)
                    .Select(t => new Category
                    {
                        Id = (int)t.Element("Id"),
                        Name = (string)t.Element("Name"),
                    })
                    .FirstOrDefault()
            );
        }

        public async Task UpdateByIdAsync(int id, Category newCategory)
        {
            _document = XDocument.Load(_path);
            XElement? categoryElement = _document?.Root
                ?.Element("Categories")
                ?.Elements("Category")
                .Where(t => (int)t.Element("Id") == id)
                .FirstOrDefault();
            if (categoryElement is not null)
            {
                categoryElement.Element("Name").Value = newCategory.Name;
                await Task.Run(() => _document.Save(_path));
            }
        }
    }
}
