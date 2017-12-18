using System;

namespace Zad1
{
    class DuplicateTodoItemException : Exception
    {
        public DuplicateTodoItemException(string message) : base(message)
        {
        }
    }
}
