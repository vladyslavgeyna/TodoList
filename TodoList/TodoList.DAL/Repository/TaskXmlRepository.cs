using System.Xml.Linq;
using TodoList.Service;
using TodoList.Service.Utils;
using Task = System.Threading.Tasks.Task;

namespace TodoList.DAL.Repository
{
    public class TaskXmlRepository : ITaskRepository
    {
        private XDocument _document;
        private readonly XmlStorageService _xmlStorageService;
        private readonly string _path;
        public TaskXmlRepository(XmlStorageService xmlStorageService)
        {
            _xmlStorageService = xmlStorageService;
            string filePath = _xmlStorageService.XmlStoragePath;
            _document = XDocument.Load(filePath);
            _path = filePath;
        }

        private async Task<bool> IsTaskByIdExist(int id)
        {
            return (await GetByIdAsync(id)) is not null;
        }

        public async Task AddAsync(Domain.Entity.Task task)
        {
            int newTaskId;
            Random random = new();
            do
{
                newTaskId = random.Next(1, int.MaxValue);
            } while (await IsTaskByIdExist(newTaskId));
            task.DateOfCreation = DateTime.Parse(DateTime.Now.ToString(DateTimeHelper.DatePattern));
            XElement taskElement = new XElement("Task",
                new XElement("Id", newTaskId),
                new XElement("CategoryId", task.CategoryId),
                new XElement("Description", task.Description),
                new XElement("DueDate", task.DueDate.ToString(DateTimeHelper.DatePattern)),
                new XElement("DateOfCreation", task.DateOfCreation.ToString(DateTimeHelper.DatePattern)),
                new XElement("IsCompleted", task.IsCompleted)
            );
            _document?.Root?.Element("Tasks")?.Add(taskElement);
            await Task.Run(() => _document?.Save(_path));
        }

        public async Task DeleteByIdAsync(int id)
        {
            _document = XDocument.Load(_path);
            XElement? taskElement = _document?.Root
                ?.Element("Tasks")
                ?.Elements("Task")
                .Where(t => (int)t.Element("Id") == id)
                .FirstOrDefault();
            if (taskElement is not null)
            {
                taskElement.Remove();
                await Task.Run(() => _document?.Save(_path));
            }
        }

        public async Task<IEnumerable<Domain.Entity.Task>> GetAllAsync()
        {
            var tasks = await Task.Run(() =>
               _document.Root
                   .Element("Tasks")
                   .Elements("Task")
                   .Select(t => new Domain.Entity.Task
                   {
                       Id = (int)t.Element("Id"),
                       CategoryId = (int?)t.Element("CategoryId"),
                       Description = (string)t.Element("Description"),
                       DueDate = (DateTime)t.Element("DueDate"),
                       DateOfCreation = (DateTime)t.Element("DateOfCreation"),
                       IsCompleted = (bool)t.Element("IsCompleted"),
                   }).ToList()
            );
            return tasks;
        }

        public async Task<Domain.Entity.Task?> GetByIdAsync(int id)
        {
            return await Task.Run(() =>
                _document?.Root
                    ?.Element("Tasks")
                    ?.Elements("Task")
                    .Where(t => (int)t.Element("Id") == id)
                    .Select(t => new Domain.Entity.Task
                    {
                        Id = (int)t.Element("Id"),
                        CategoryId = (int)t.Element("CategoryId"),
                        Description = (string)t.Element("Description"),
                        DateOfCreation = (DateTime)t.Element("DateOfCreation"),
                        DueDate = (DateTime)t.Element("DueDate"),
                        IsCompleted = (bool)t.Element("IsCompleted")
                    })
                    .FirstOrDefault()
            );
        }

        public async Task UpdateByIdAsync(int id, Domain.Entity.Task newTask)
        {
            _document = XDocument.Load(_path);
            XElement? taskElement = _document?.Root
                ?.Element("Tasks")
                ?.Elements("Task")
                .Where(t => (int)t.Element("Id") == id)
                .FirstOrDefault();
            if (taskElement is not null)
            {
                taskElement.Element("CategoryId").Value = newTask.CategoryId.ToString();
                taskElement.Element("Description").Value = newTask.Description;
                taskElement.Element("DueDate").Value = newTask.DueDate.ToString();
                taskElement.Element("IsCompleted").Value = newTask.IsCompleted.ToString();
                await Task.Run(() => _document.Save(_path));
            }
        }
    }
}
