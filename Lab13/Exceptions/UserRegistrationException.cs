namespace Lab13.Exceptions;

public class UserRegistrationException : Exception
{
    public UserRegistrationException(string? message) : base(message)
    {
    }
}