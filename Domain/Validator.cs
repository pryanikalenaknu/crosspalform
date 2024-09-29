namespace Domain;

public static class Validator
{
    
    public static void IsTrue(bool expres, string message)
    {
        if (!expres)
        {
            throw new ArgumentException(message);
        }
    }
}