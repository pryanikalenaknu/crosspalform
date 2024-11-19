namespace Lab5.Exceptions;

public class UserLoginException : Exception
{
    public UserLoginException(string? message) : base(message)
    {
    }
}