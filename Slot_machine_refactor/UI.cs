namespace Slot_machine_refactor;

public class UI
{
    
    public static void DisplayUserIntroMessage()
    {
        Console.WriteLine("Let's play a game! The name of the game is a slot machine");
    }

    public static CustomClasses.DoubleValidation ValidateDoubleEntry(string entry)
    {
        double val;
        bool success = double.TryParse(entry, out val);

        var doubleEntry = new CustomClasses.DoubleValidation();

        if (success)
        {
            doubleEntry.DoubleValue = val;
            doubleEntry.SuccessfulDouble = success;
        }
        else
        {
            Console.WriteLine("Please enter a valid Double entry only");
            doubleEntry.DoubleValue = -1;
            doubleEntry.SuccessfulDouble = false;
        }

        return doubleEntry;
    }
    
    public static CustomClasses.DoubleValidation ValidateUserWalletEntry()
    {
        string value;
        bool success = false;
        
        var final  = new CustomClasses.DoubleValidation();

        while (!success)
        {
            Console.WriteLine("Please enter how much you would like to load in your wallet now: ");
            value = Console.ReadLine();

            final = ValidateDoubleEntry(value);
            success = final.SuccessfulDouble;
        }
        return final;
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

    public static CustomClasses.DoubleValidation GetUserBetEntry()
    {
        string value;
        bool success = false;
        
        var final  = new CustomClasses.DoubleValidation();

        while (!success)
        {
            Console.WriteLine("Please enter your bet now: ");
            value = Console.ReadLine();

            final = ValidateDoubleEntry(value);
            success = final.SuccessfulDouble;
        }
        return final;
        
    }
    
}