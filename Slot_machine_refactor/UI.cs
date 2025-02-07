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
        Console.WriteLine($"\nYou currently have ${amount} loaded in your wallet.");
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
    
    public static char GetContinueGameStatus(double walletAmount)
    {
        char continueGame = ' ';
        if (walletAmount <= 0.1)
        {
            continueGame = Constants.NO_FUNDS_LEFT;
        }
        else
        {
            continueGame = char.ToUpper(Console.ReadKey().KeyChar);
        }
        return continueGame;
    }
    
    
    public static GameWinValidation.GameWinValidationClass DisplayGameWinStatus(bool gameStatus, double walletAmount, double wagerAmount, double payoutRate)
    {
        var Gamewin = new GameWinValidation.GameWinValidationClass();
        (Gamewin.Payout , Gamewin.Wallet) = Logic.CalculateGameWinDoubles(gameStatus, walletAmount, wagerAmount, payoutRate);
        
        if (gameStatus)
        {
            Console.WriteLine("You Win!");
            Console.WriteLine($"Your total payout is: {Gamewin.Payout}");
            Console.WriteLine($"The total in your wallet is: {Gamewin.Wallet}\n");
        }
        else
        {
            Console.WriteLine("You Lose!");
        }

        if (Gamewin.Wallet <= 0.10)
        {
            Console.WriteLine("Game Over! You currently have no funds left.");
            return Gamewin;
        }
        else
        {
            Console.WriteLine($"\nYou currently have ${Gamewin.Wallet} in your wallet");
            Console.WriteLine($"would you like to play again? {Constants.CONTINUE_PLAYING_GAME}/N\n");
        }
        Gamewin.ContinueGame = GetContinueGameStatus(walletAmount);
        
        return Gamewin;
        
    }


}
