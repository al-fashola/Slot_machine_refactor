namespace Slot_machine_refactor;

public class UI
{
    
    public static void DisplayUserIntroMessage()
    {
        Console.WriteLine("Let's play a game! The name of the game is a slot machine");
    }

    public static double ValidateDoubleEntry(string entry)
    {
        double val;
        bool success = double.TryParse(entry, out val);
        double finalVal;

        if (success)
        {
            finalVal = val;
        }
        else
        {
            Console.WriteLine("Please enter a valid Double entry only");
            finalVal = -1;
        }
        return (finalVal);
    }
    
    public static double ValidateUserWalletEntry()
    {
        string value;
        bool success = false;
        double finalVal = -1.0;

        while (!success)
        {
            Console.WriteLine("Please enter how much you would like to load in your wallet now: ");
            value = Console.ReadLine();

            double DoubleValue = ValidateDoubleEntry(value);
            if (DoubleValue > -1)
            {
                success = true;
                finalVal = DoubleValue;
            }
        }
        return (finalVal);
    }
    
    public static void DisplayWalletAmount( double amount)
    {
        Console.WriteLine($"You currently have ${amount} loaded in your wallet .");
    }

    public static double GetUserBetEntry()
    {
        string value;
        bool success = false;
        double finalVal = -1.0;

        while (!success)
        {
            Console.WriteLine("Please enter your bet now: ");
            value = Console.ReadLine();

            double DoubleValue = ValidateDoubleEntry(value);
            if(DoubleValue > -1)
            {
                success = true;
                finalVal = DoubleValue;
            }
        }
        return (finalVal);
        
    }
    
    
    public static void DisplayBetEntryInvalidMessage(double amount)
    {
        Console.WriteLine(
            $"The wager amount will need to be greater than 0 and less than or equal to the funds currently in your wallet. Wallet: {amount}");
    }
    
    
    public static int ValidateIntEntry(string entry)
    {
        int val;
        bool success = int.TryParse(entry, out val);
        int finalVal; 

        if (success)
        {
            finalVal = val;
        }
        else
        {
            Console.WriteLine("Please enter a valid Integer entry from the list only");
            finalVal = -1;
        }

        return (finalVal);
    }
    
    public static int ValidateUserGameEntry()
    {
        string value;
        bool success = false;
        int finalVal = 0; 

        while (!success)
        {
            Console.WriteLine("Enter your choice number: ");
            value = Console.ReadLine();

            int IntValue= ValidateIntEntry(value);
            if (IntValue > 0 )
            {
                finalVal = IntValue;
                success = true;
            }
        }
        return (finalVal);
    }

    public static void DiplaySlotGrid(int[,] grid)
    {
        for (int i = 0; i < Constants.MATRIX_GRID_SIZE; i++)
        {
            for (int j = 0; j < Constants.MATRIX_GRID_SIZE; j++)
            {
                Console.Write($"|{grid[i, j]}|");
            }

            Console.WriteLine("");
        }
    }


    // break this out into multiple methods - 3 
    public static GameWinValidation.GameWinValidationClass DisplayGameWinStatus(bool gameStatus, double walletAmount, double wagerAmount, double payoutRate)
    {
        var Gamewin = new GameWinValidation.GameWinValidationClass();
        
        Gamewin.Wallet = walletAmount;
        
        if (gameStatus == false)
        {
            Console.WriteLine("You Lose!");
            Gamewin.Wallet = (Gamewin.Wallet - wagerAmount);
        }

        if (gameStatus)
        {
            Console.WriteLine("You Win!");
            Gamewin.Payout = payoutRate * wagerAmount;
            Gamewin.Wallet = Gamewin.Payout + Gamewin.Wallet;
            Console.WriteLine($"Your total payout is: {Gamewin.Payout}");
            Console.WriteLine($"The total in your wallet is: {Gamewin.Wallet}\n");
        }

        if (Gamewin.Wallet == 0.0)
        {
            Console.WriteLine("Game Over! You currently have no funds left.");
            Gamewin.ContinueGame = Constants.NO_FUNDS_LEFT;
        }
        else
        {
            Console.WriteLine($"You currently have ${Gamewin.Wallet} in your wallet");
            Console.WriteLine($"would you like to play again? {Constants.CONTINUE_PLAYING_GAME}/N");
            Gamewin.ContinueGame = Console.ReadKey().KeyChar;
            Gamewin.ContinueGame = char.ToUpper(Gamewin.ContinueGame);
        }
        
        return Gamewin;
        
    }


}
