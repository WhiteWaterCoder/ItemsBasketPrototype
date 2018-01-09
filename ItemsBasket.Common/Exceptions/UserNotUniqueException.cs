using System;

namespace ItemsBasket.Common.Exceptions
{
    public class UserNotUniqueException : Exception
    {
        public UserNotUniqueException(string message)
            : base(message)
        {

        }
    }
}