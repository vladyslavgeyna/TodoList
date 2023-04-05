using System.ComponentModel.DataAnnotations;

namespace TodoList.Attributes
{
    public class LessCurrentDateAttribute : ValidationAttribute
    {
        public LessCurrentDateAttribute() : base("Date can not be less or equals than current date")
        {

        }

        public override bool IsValid(object? value)
        {
            DateTime propValue = Convert.ToDateTime(value);
            if (propValue <= DateTime.Now)
            {
                return false;
            }
            return true;
        }
    }
}
