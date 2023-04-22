using System.ComponentModel.DataAnnotations;
using TodoList.Enums;

namespace TodoList.ViewModels
{
    public class ChangeStorageViewModel
    {
        [Required(ErrorMessage = "Storage type is required.")]
        public Storage StorageType { get; set; }
    }
}
