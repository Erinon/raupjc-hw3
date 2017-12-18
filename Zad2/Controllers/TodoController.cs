using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Zad1;
using Microsoft.AspNetCore.Identity;
using Zad2.Models;
using Zad2.Models.TodoViewModels;

namespace Zad2.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoController(ITodoRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);

            IndexViewModel model = new IndexViewModel(_repository.GetActive(Guid.Parse(currentUser.Id))
                .Select(i => new TodoViewModel()
                {
                    Id = i.Id,
                    Text = i.Text,
                    DateDue = i.DateDue,
                    DateCompleted = i.DateCompleted
                }).ToList());

            return View(model);
        }

        public async Task<IActionResult> Completed()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);

            CompletedViewModel model = new CompletedViewModel(_repository.GetCompleted(Guid.Parse(currentUser.Id))
                .Select(i => new TodoViewModel()
                {
                    Id = i.Id,
                    Text = i.Text,
                    DateDue = i.DateDue,
                    DateCompleted = i.DateCompleted
                }).ToList());

            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTodoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", model);
            }

            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);

            TodoItem item = new TodoItem(model.Text, Guid.Parse(currentUser.Id))
            {
                DateDue = model.DateDue
            };

            _repository.Add(item);

            if (model.Labels != null)
            {
                string[] labels = model.Labels.Split(new string[] { ", " }, StringSplitOptions.None);

                foreach (string label in labels)
                {
                    _repository.AddLabel(label, item.Id);
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MarkAsCompleted(Guid itemId)
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);

            _repository.MarkAsCompleted(itemId, Guid.Parse(currentUser.Id));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MarkAsActive(Guid itemId)
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);

            _repository.MarkAsActive(itemId, Guid.Parse(currentUser.Id));

            return RedirectToAction("Completed");
        }
    }
}