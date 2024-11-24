namespace Lab13.Exceptions;

public class UserLoginException : Exception
{
    public UserLoginException(string? message) : base(message)
    {
    }
}