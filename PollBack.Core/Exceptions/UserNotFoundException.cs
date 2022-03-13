﻿namespace PollBack.Core.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User not found.")
        {
        }
    }
}
