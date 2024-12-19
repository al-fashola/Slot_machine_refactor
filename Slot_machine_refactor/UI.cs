namespace Slot_machine_refactor;

public class UI
{
    
    public static void UserIntroMessage()
    {
        Console.WriteLine("Let's play a game! The name of the game is a slot machine");
    }
    
    public static string UserIntroWalletEntry()
    {
        string value;
        Console.WriteLine("Please enter how much you would like to load in your wallet now: ");
        value = Console.ReadLine();
        
        return value;
    }

    public static string UserValidateWalletEntry()
    {
        string value;
        Console.WriteLine("Please enter digits only!");
        Console.Write("Please enter your Wallet amount now: ");
        value = Console.ReadLine();
        
        return value;
    }
    
    public static void DisplayWalletAmount( double amount)
    {
        Console.WriteLine($"You currently have ${amount} loaded in your wallet .");
    }
    
    public static void DisplayGameModes(int gameMode1, int gameMode2, int gameMode3, int gameMode4)
    {
        Console.WriteLine("You will have the following options: \n" +
                          $"{gameMode1} - Play the center line \n" +
                          $"{gameMode2} - Play all three horizontals \n" +
                          $"{gameMode3} - Play all vertical lines \n" +
                          $"{gameMode4} - Play diagonal lines \n");
    }

    public static string UserBetEntry()
    {
        string value;
        Console.WriteLine("Please enter your bet now: ");
        value= Console.ReadLine();
        return value;
    }
    
}