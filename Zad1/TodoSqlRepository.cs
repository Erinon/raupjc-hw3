using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Zad1
{
    public class TodoSqlRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }

        public void Add(TodoItem todoItem)
        {
            if (_context.TodoItems.Find(todoItem.Id) != null)
            {
                throw new DuplicateTodoItemException($"duplicate id: {todoItem.Id }");
            }

            _context.TodoItems.Add(todoItem);

            _context.SaveChanges();
        }

        public TodoItem Get(Guid todoId, Guid userId)
        {
            TodoItem item = _context.TodoItems.Find(todoId);

            if (item == null)
            {
                return null;
            }
            else if (!item.UserId.Equals(userId))
            {
                throw new TodoAccessDeniedException("You must be the owner of the Todo item");
            }

            return item;
        }

        public List<TodoItem> GetAll(Guid userId)
        {
            return _context.TodoItems.Where(i => i.UserId.Equals(userId))
                                     .OrderByDescending(i => i.DateCreated)
                                     .ToList();
        }

        public List<TodoItem> GetActive(Guid userId)
        {
            return GetAll(userId).Where(i => !i.IsCompleted)
                                 .ToList();
        }

        public List<TodoItem> GetCompleted(Guid userId)
        {
            return GetAll(userId).Where(i => i.IsCompleted)
                                 .ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            return GetAll(userId).Where(filterFunction)
                                 .ToList();
        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            TodoItem item = Get(todoId, userId);

            if (item == null)
            {
                return false;
            }

            bool ret = item.MarkAsCompleted();

            _context.SaveChanges();

            return ret;
        }

        public bool MarkAsActive(Guid todoId, Guid userId)
        {
            TodoItem item = Get(todoId, userId);

            if (item == null)
            {
                return false;
            }

            bool ret = item.MarkAsActive();

            _context.SaveChanges();

            return ret;
        }

        public bool Remove(Guid todoId, Guid userId)
        {
            TodoItem item = Get(todoId, userId);

            if (item == null)
            {
                return false;
            }

            _context.TodoItems.Remove(item);

            _context.SaveChanges();

            return true;
        }

        public void Update(TodoItem todoItem, Guid userId)
        {
            TodoItem item = Get(todoItem.Id, userId);

            _context.TodoItems.AddOrUpdate(item);

            _context.SaveChanges();
        }

        public void AddLabel(string value, Guid todoId)
        {
            TodoItem item = _context.TodoItems.FirstOrDefault(i => i.Id.Equals(todoId));

            TodoItemLabel label = _context.TodoItemLabels.FirstOrDefault(l => l.Value.Equals(value));

            if (label == null)
            {
                label = new TodoItemLabel(value);

                _context.TodoItemLabels.Add(label);
            }

            item.Labels.Add(label);

            label.LabelTodoItems.Add(item);

            _context.SaveChanges();
        }
    }
}
