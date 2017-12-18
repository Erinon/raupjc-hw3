using System.Collections.Generic;

namespace Zad2.Models.TodoViewModels
{
    public class IndexViewModel
    {
        public List<TodoViewModel> TodoViewModels { get; }

        public IndexViewModel(List<TodoViewModel> models)
        {
            TodoViewModels = models;
        }
    }
}
