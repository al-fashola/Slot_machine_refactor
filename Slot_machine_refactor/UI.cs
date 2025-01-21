namespace Slot_machine_refactor;

public class UI
{
    
    public static void DisplayUserIntroMessage()
    {
        Console.WriteLine("Let's play a game! The name of the game is a slot machine");
    }

    public static (double DoubleValue, bool SuccessfulDouble) ValidateDoubleEntry(string entry)
    {
        double val;
        bool success = double.TryParse(entry, out val);
        double finalVal;
        //var doubleEntry = new CustomClasses.DoubleValidation();

        if (success)
        {
            finalVal = val;
        }
        else
        {
            Console.WriteLine("Please enter a valid Double entry only");
            finalVal = -1;
        }
        return (finalVal, success);
    }
    
    public static (double DoubleValue, bool SuccessfulDouble) ValidateUserWalletEntry()
    {
        string value;
        bool success = false;
        double finalVal = 0.0;
        
        //var final  = new CustomClasses.DoubleValidation();

        while (!success)
        {
            Console.WriteLine("Please enter how much you would like to load in your wallet now: ");
            value = Console.ReadLine();

            var final = ValidateDoubleEntry(value);
            success = final.SuccessfulDouble;
            finalVal = final.DoubleValue;
        }
        return ( finalVal , success);
    }
    
    public static void DisplayWalletAmount( double amount)
    {
        Console.WriteLine($"You currently have ${amount} loaded in your wallet .");
    }

    public static Dictionary<int, string> CreateGameModes()
    {
        Dictionary<int, string> gameModes = new Dictionary<int, string>();
    
        gameModes.Add(Constants.CENTER_LINE_MODE, "Play the center line");
        gameModes.Add(Constants.HORIZONTAL_LINE_MODE, "Play all three horizontals");
        gameModes.Add(Constants.VERTICAL_LINE_MODE, "Play all vertical lines");
        gameModes.Add(Constants.ALL_DIAGONOL_LINE_MODE, "Play diagonal lines");
        
        // Be sure to update constant NUMBER_OF_GAMES_OPTIONS with the total number of games 
        
        return gameModes;
    }
    
    
    public static void DisplayGameModes(Dictionary<int, string> gameModes)
    {
        Console.WriteLine("You will have the following options: \n");
        foreach (KeyValuePair<int, string> gameMode in gameModes)
        {
            Console.WriteLine($"{gameMode.Key} - {gameMode.Value}");
        }
    }

    public static (double DoubleValue, bool SuccessfulDouble) GetUserBetEntry()
    {
        string value;
        bool success = false;
        double finalVal = 0.0;
        
        //var final  = new CustomClasses.DoubleValidation();

        while (!success)
        {
            Console.WriteLine("Please enter your bet now: ");
            value = Console.ReadLine();

            var final = ValidateDoubleEntry(value);
            success = final.SuccessfulDouble;
            finalVal = final.DoubleValue;
        }
        return (finalVal, success);
        
    }
    
    
    public static void DisplayBetEntryInvalidMessage(double amount)
    {
        Console.WriteLine(
            $"The wager amount will need to be greater than 0 and less than or equal to the funds currently in your wallet. Wallet: {amount}");
    }
    
    
    public static (int IntValue, bool SuccessfulInteger) ValidateIntEntry(string entry)
    {
        int val;
        bool success = int.TryParse(entry, out val);
        int finalVal; 

        //var IntEntry = new CustomClasses.IntValidation();

        if (success)
        {
            finalVal = val;
        }
        else
        {
            Console.WriteLine("Please enter a valid Integer entry from the list only");
            finalVal = -1;
        }

        return (finalVal, success);
    }
    
    public static (int IntValue, bool SuccessfulInteger) ValidateUserGameEntry()
    {
        string value;
        bool success = false;
        int finalVal = 0; 
        
        //var final  = new CustomClasses.IntValidation();

        while (!success)
        {
            Console.WriteLine("Enter your choice number: ");
            value = Console.ReadLine();

            var final = ValidateIntEntry(value);
            finalVal = final.IntValue;
            success = final.SuccessfulInteger;
        }
        return ( finalVal, success);
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
