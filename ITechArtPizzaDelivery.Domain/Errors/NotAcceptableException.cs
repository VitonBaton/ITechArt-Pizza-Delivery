using System;

namespace ITechArtPizzaDelivery.Domain.Errors
{
    public class NotAcceptableException : Exception
    {
        public NotAcceptableException() {}
        public NotAcceptableException(string message) : base(message) {}
    }
}