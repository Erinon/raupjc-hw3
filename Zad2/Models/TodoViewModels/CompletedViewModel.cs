using System.Collections.Generic;

namespace Zad2.Models.TodoViewModels
{
    public class CompletedViewModel
    {
        public List<TodoViewModel> TodoViewModels { get; }

        public CompletedViewModel(List<TodoViewModel> models)
        {
            TodoViewModels = models;
        }
    }
}
