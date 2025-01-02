namespace Slot_machine_refactor;

public class Logic
{
    
    public static Random rand = new Random();
    
    public static bool ValidateSuccessfulWagerEntry(bool wagerEntrySuccess, double WagerAmount, double WalletAmount)
    {
        bool success = (!wagerEntrySuccess || WagerAmount > WalletAmount || WagerAmount == 0.0);
        return success;
    }
    
    public static bool ValidateSuccessfulGameEntry(bool GameEntrySuccess, int GameMode)
    {
        bool success = (!GameEntrySuccess || (GameMode <= 0 || GameMode > Constants.NUMBER_OF_GAMES_OPTIONS));
        
        return success;
    }
    
    public static int[,] PopulateSlotMachineGrid ()
    {
        int[,] SlotGrid = new int[Constants.MATRIX_GRID_SIZE, Constants.MATRIX_GRID_SIZE];
        
        for (int i = 0; i < Constants.MATRIX_GRID_SIZE; i++)
        {
            for (int j = 0; j < Constants.MATRIX_GRID_SIZE; j++)
            {
                SlotGrid[i, j] = rand.Next(0, Constants.MAX_SLOT_MACHINE_INT);
            }
        }
        return SlotGrid;
    }
    
    
    public static double payoutRate = 0.0;
    public static bool gameWin = false;
    public static int centerValueInt = Constants.MATRIX_GRID_SIZE / 2;
    
    // Validate gamewin - return bool, payoutrate/total payout - create variable for this return function 
    //this will contain all the if validations but could break them each into smaller methods as well before being called in the main gamewin
    
    
    public static CustomClasses.GameModeValidation ValidateCenterLineGameMode(int gameMode, int [,] grid )
    {
        int matchCounter = 0;
        var GameResults = new CustomClasses.GameModeValidation();
        
        if (gameMode == Constants.CENTER_LINE_MODE)
        {
            int middleValue = grid[centerValueInt, centerValueInt];

            for (int i = 0; i < Constants.MATRIX_GRID_SIZE; i++)
            {
                if (middleValue == grid[centerValueInt, i]) matchCounter++;
            }

            if (matchCounter == Constants.MATRIX_GRID_SIZE)
            {
                payoutRate = Constants.CENTER_LINE_PAYOUT;
                gameWin = true;
            }
        }
        
        GameResults.GameWin = gameWin;
        GameResults.Payout = payoutRate;
        
        return GameResults;
    }
    
    public static CustomClasses.GameModeValidation ValidateHorizontalLineGameMode(int gameMode, int [,] grid )
    {
        int firstValue = 0;
        int matchCounter = 0;
        var GameResults = new CustomClasses.GameModeValidation();
        
        if (gameMode == Constants.HORIZONTAL_LINE_MODE)
        {
            for (int i = 0; i < Constants.MATRIX_GRID_SIZE; i++)
            {
                matchCounter = 0;
                firstValue = grid[i, 0];
                for (int j = 0; j < Constants.MATRIX_GRID_SIZE; j++)
                {
                    if (firstValue == grid[i, j]) matchCounter++;
                }

                if (matchCounter == Constants.MATRIX_GRID_SIZE)
                {
                    payoutRate = Constants.HORIZONTAL_LINE_PAYOUT;
                    gameWin = true;
                    break;
                }
            }
        }
        GameResults.GameWin = gameWin;
        GameResults.Payout = payoutRate;
        return GameResults;
    }
    
    
    public static CustomClasses.GameModeValidation ValidateVerticalLineGameMode(int gameMode, int [,] grid )
    {
        int firstValue = 0;
        int matchCounter = 0;
        var GameResults = new CustomClasses.GameModeValidation();
        
        if (gameMode == Constants.VERTICAL_LINE_MODE)
        {
            for (int i = 0; i < Constants.MATRIX_GRID_SIZE; i++)
            {
                matchCounter = 0;
                firstValue = grid[0, i];
                for (int j = 0; j < Constants.MATRIX_GRID_SIZE; j++)
                {
                    if (firstValue == grid[j, i]) matchCounter++;
                }

                if (matchCounter == Constants.MATRIX_GRID_SIZE)
                {
                    payoutRate = Constants.VERTICAL_LINE_PAYOUT;
                    gameWin = true;
                    break;
                }
            }
        }
        GameResults.GameWin = gameWin;
        GameResults.Payout = payoutRate;
        return GameResults;
    }
    
    
    //left off
    public static CustomClasses.GameModeValidation ValidateDiagonalLineGameMode(int gameMode, int [,] grid )
    {
        int matchCounter = 0;
        var GameResults = new CustomClasses.GameModeValidation();
        
        if (gameMode == Constants.ALL_DIAGONOL_LINE_MODE)
        {
            matchCounter = 0;
            int matchCounterAlt = 0;
            int matchValue = grid[centerValueInt, centerValueInt];

            for (int h = 0, i = 0, j = Constants.MATRIX_GRID_SIZE - 1; h < Constants.MATRIX_GRID_SIZE; h++, i++, j--)
            {
                if (matchValue == grid[h, i]) matchCounter++;
                if (matchValue == grid[j, i]) matchCounterAlt++;
            }

            if (matchCounter == Constants.MATRIX_GRID_SIZE || matchCounterAlt == Constants.MATRIX_GRID_SIZE)
            {
                payoutRate = Constants.ALL_DIAGONOL_LINE_PAYOUT;
                gameWin = true;
            }
        }
        GameResults.GameWin = gameWin;
        GameResults.Payout = payoutRate;
        return GameResults;
    }



}


