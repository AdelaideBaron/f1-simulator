namespace View;

public class UserInteractions
{
    public static bool DoesUserWantAction()
    {
        Console.WriteLine("Enter y for yes, anything else for no"); // Todo update this to be more dynamic 
        string userInput = Console.ReadLine()!;
        return userInput.ToLower().Equals("y") ? true : false;
    }
}