using System;

namespace BusinessLayer.Infrastructure
{
    public class ValidationException : Exception
    {
        public string Property { get; set; }
        public ValidationException(string message, string property) : base(message)
        {
            Property = property;
        }
    }
}
