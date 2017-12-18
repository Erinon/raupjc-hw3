using System;
using System.Collections.Generic;

namespace Zad1
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsCompleted => DateCompleted.HasValue;
        public DateTime? DateCompleted { get; set; }
        public DateTime DateCreated { get; set; }

        /// <summary >
        /// User id that owns this TodoItem.
        /// </ summary >
        public Guid UserId { get; set; }

        /// <summary >
        /// /// List of labels associated with TodoItem.
        /// </ summary >
        public List<TodoItemLabel> Labels { get; set; }

        /// <summary >
        /// Date due. If null, no date was set by the user.
        /// </ summary >
        public DateTime? DateDue { get; set; }

        public TodoItem(string text, Guid userId)
        {
            Id = Guid.NewGuid();
            Text = text;
            DateCreated = DateTime.UtcNow;
            UserId = userId;
            Labels = new List<TodoItemLabel>();
        }

        public TodoItem()
        {
            // entity framework needs this one
            // not for use :)
        }

        public bool MarkAsCompleted()
        {
            if(IsCompleted)
            {
                return false;
            }

            DateCompleted = DateTime.Now;

            return true;
        }

        public bool MarkAsActive()
        {
            if(!IsCompleted)
            {
                return false;
            }

            DateCompleted = null;

            return true;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is TodoItem))
            {
                return false;
            }

            return ((TodoItem)obj).Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
