using System;

namespace Zad1
{
    class TodoAccessDeniedException : Exception
    {
        public TodoAccessDeniedException(string message) : base(message)
        {
        }
    }
}
