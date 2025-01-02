namespace Slot_machine_refactor;

class Program
{
    static void Main(string[] args)
    {
        //intro message to establish the game being played 
        UI.DisplayUserIntroMessage();
        
        bool wagerEntrySuccessful;
        bool gameChoiceSuccessful;
    
        
        var walletEntry  = UI.ValidateUserWalletEntry();
        double wallet = walletEntry.DoubleValue;
       
        
        char input = Constants.CONTINUE_PLAYING_GAME;
        while (input == Constants.CONTINUE_PLAYING_GAME)
        {
            UI.DisplayWalletAmount(wallet);
            UI.DisplayGameModes(Constants.CENTER_LINE_MODE, Constants.HORIZONTAL_LINE_MODE, Constants.VERTICAL_LINE_MODE, Constants.ALL_DIAGONOL_LINE_MODE);

            double wager = 0.0;

            var wagerEntry = UI.GetUserBetEntry();
            wagerEntrySuccessful = wagerEntry.SuccessfulDouble;  
            wager = wagerEntry.DoubleValue;
            
            while (Logic.ValidateSuccessfulWagerEntry(wagerEntrySuccessful, wager, wallet))
            {
                UI.DisplayBetEntryInvalidMessage(wallet);
                
                wagerEntry = UI.GetUserBetEntry();
                wagerEntrySuccessful = wagerEntry.SuccessfulDouble;
                wager = wagerEntry.DoubleValue;
            }

            int gameMode = 0;
                
            var GameEntry  = UI.ValidateUserGameEntry();
            gameChoiceSuccessful = GameEntry.SuccessfulInteger;
            gameMode = GameEntry.IntValue;
               
            while (Logic.ValidateSuccessfulGameEntry(gameChoiceSuccessful, gameMode))
            {
                GameEntry = UI.ValidateUserGameEntry();
                gameChoiceSuccessful = GameEntry.SuccessfulInteger; 
                gameMode = GameEntry.IntValue;
            }

            // assign the size of array
            int[,] grid = Logic.PopulateSlotMachineGrid(); 

            //print current grid 
            UI.DiplaySlotGrid(grid);
           
            
            
            ///

            double payoutRate = 0.0;
            double totalPayout = 0.0;
            bool gameWin = false;

            int matchCounter = 0;
            int firstValue = 0;

            
            // Logical breakdown to validate match in values across diagonals, horizontals, verticals
            if (gameMode == Constants.CENTER_LINE_MODE)
            {
                int centerValueInt = Constants.MATRIX_GRID_SIZE / 2;
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

            if (gameMode == Constants.ALL_DIAGONOL_LINE_MODE)
            {
                //((grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2]) 
                // || (grid[2, 0] == grid[1, 1] && grid[1, 0] == grid[0, 2]))
                matchCounter = 0;
                int matchCounterAlt = 0;

                int centerValueInt = Constants.MATRIX_GRID_SIZE / 2;
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

            //Win or lose logic
            //UI.DiplayGameWinStatus()
            if (gameWin == false)
            {
                Console.WriteLine("You Lose!");
                wallet = (wallet - wager);
            }

            if (gameWin)
            {
                Console.WriteLine("You Win!");
                totalPayout = payoutRate * wager;
                wallet = totalPayout + wallet;
                Console.WriteLine($"Your total payout is: {totalPayout}");
                Console.WriteLine($"The total in your wallet is: {wallet}");
            }

            if (wallet == 0.0)
            {
                Console.WriteLine("Game Over! You currently have no funds left.");
                input = Constants.NO_FUNDS_LEFT;
            }
            else
            {
                Console.WriteLine($"You currently have ${wallet} in your wallet");
                Console.WriteLine($"would you like to play again? {Constants.CONTINUE_PLAYING_GAME}/N");
                input = Console.ReadKey().KeyChar;
                input = char.ToUpper(input);
            }
        }
    }
}