﻿namespace PollBack.Core.Exceptions
{
    public class WrongPasswordException : Exception
    {
        public WrongPasswordException() : base("Wrong password.")
        {
        }
    }
}
