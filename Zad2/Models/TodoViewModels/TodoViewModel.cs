using System;

namespace Zad2.Models.TodoViewModels
{
    public class TodoViewModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime? DateDue { get; set; }
        public bool IsCompleted => DateCompleted.HasValue;
        public DateTime? DateCompleted { get; set; }

        public int Remaining()
        {
            return (int)((DateTime)DateDue - DateTime.UtcNow).TotalDays;
        }
    }
}
