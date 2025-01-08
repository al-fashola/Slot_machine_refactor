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
           
            
            
            

            double payoutRate = 0.0;
            double totalPayout = 0.0;
            bool gameWin = false;

            int matchCounter = 0;
            int firstValue = 0;

            
            if (gameMode == Constants.CENTER_LINE_MODE)
            {
             var gameModeResults = Logic.ValidateCenterLineGameMode(gameMode, grid);

             if (gameModeResults.GameWin)
             {
                 gameWin = true;
                 payoutRate = gameModeResults.Payout;
             }
            }

            if (gameMode == Constants.HORIZONTAL_LINE_MODE)
            {
                var gameModeResults = Logic.ValidateHorizontalLineGameMode(gameMode, grid);

                if (gameModeResults.GameWin)
                {
                    gameWin = true;
                    payoutRate = gameModeResults.Payout;
                }
            }

            if (gameMode == Constants.VERTICAL_LINE_MODE)
            {
                var gameModeResults = Logic.ValidateVerticalLineGameMode(gameMode, grid);

                if (gameModeResults.GameWin)
                {
                    gameWin = true;
                    payoutRate = gameModeResults.Payout;
                }
            } 

            if (gameMode == Constants.ALL_DIAGONOL_LINE_MODE)
            {
                var gameModeResults = Logic.ValidateDiagonalLineGameMode(gameMode, grid);
                if (gameModeResults.GameWin)
                {
                    gameWin = true;
                    payoutRate = gameModeResults.Payout;
                }
            }

            //Win or lose logic
            var gameWinStatus = UI.DisplayGameWinStatus(gameWin, wallet, wager, payoutRate );
            input = gameWinStatus.ContinueGame;
            wallet = gameWinStatus.Wallet;
            //totalPayout = gameWinStatus.Payout;
        }
    }
}