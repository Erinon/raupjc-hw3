using System;
using System.Collections.Generic;

namespace Zad1
{
    /// <summary >
    /// Label describing the TodoItem.
    /// e.g. Critical , Personal , Work...
    /// </ summary >
    public class TodoItemLabel
    {
        public Guid Id { get; set; }
        public string Value { get; set; }

        /// <summary >
        /// All TodoItems that are associated with this label.
        /// </ summary >
        public List<TodoItem> LabelTodoItems { get; set; }

        public TodoItemLabel(string value)
        {
            Id = Guid.NewGuid();
            Value = value;
            LabelTodoItems = new List<TodoItem>();
        }

        public TodoItemLabel()
        {
            LabelTodoItems = new List<TodoItem>();
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is TodoItemLabel))
            {
                return false;
            }

            return ((TodoItemLabel)obj).Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
