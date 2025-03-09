namespace Slot_machine_refactor;

class Program
{
    static void Main(string[] args)
    {
        //intro message to establish the game being played 
        UI.DisplayUserIntroMessage();
        
        // necessary?
        bool wagerEntrySuccessful = false;
        bool gameChoiceSuccessful = false;
        
        double wallet = UI.ValidateUserWalletEntry();
        
        //Define game modes as dictionary 
        Dictionary<int, string> gameModes = new Dictionary<int, string>();
        {
            gameModes.Add(Constants.CENTER_LINE_MODE, "Play the center line");
            gameModes.Add(Constants.HORIZONTAL_LINE_MODE, "Play all three horizontals");
            gameModes.Add(Constants.VERTICAL_LINE_MODE, "Play all vertical lines");
            gameModes.Add(Constants.ALL_DIAGONOL_LINE_MODE, "Play diagonal lines");
        }
        
        char input = Constants.CONTINUE_PLAYING_GAME;
        while (input == Constants.CONTINUE_PLAYING_GAME)
        {
            UI.DisplayWalletAmount(wallet);
            
            // Print all available game mode types 
            foreach (KeyValuePair<int, string> game in gameModes)
            {
                Console.WriteLine($"{game.Key} - {game.Value}");
            }

            double wager = 0.0;

            double wagerEntry = UI.GetUserBetEntry();
            if (wagerEntry > -0.0)
            {
                wager = wagerEntry;
                wagerEntrySuccessful = true;
            }
            
            while (Logic.ValidateSuccessfulWagerEntry(wagerEntrySuccessful, wager, wallet))
            {
                UI.DisplayBetEntryInvalidMessage(wallet);
                
                wagerEntry = UI.GetUserBetEntry();
                if (wagerEntry > -1.0)
                {
                    wager = wagerEntry;
                    wagerEntrySuccessful = true;
                }
            }

            int gameMode = 0;
                
            int GameEntry  = UI.ValidateUserGameEntry();
            if (GameEntry > -1)
            {
                gameChoiceSuccessful = true;
                gameMode = GameEntry;
            }
               
            while (Logic.ValidateSuccessfulGameEntry(gameChoiceSuccessful, gameMode))
            {
                GameEntry = UI.ValidateUserGameEntry();
                gameChoiceSuccessful = true; 
                gameMode = GameEntry;
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

            (bool GameWin, double Payout) gameModeResults = (false, 0.0);
            

            
            if (gameMode == Constants.CENTER_LINE_MODE)
            { 
                gameModeResults = Logic.ValidateCenterLineGameMode(gameMode, grid);
            }

            if (gameMode == Constants.HORIZONTAL_LINE_MODE)
            {
                gameModeResults = Logic.ValidateHorizontalLineGameMode(gameMode, grid);
            }

            if (gameMode == Constants.VERTICAL_LINE_MODE)
            {
                gameModeResults = Logic.ValidateVerticalLineGameMode(gameMode, grid);
            } 

            if (gameMode == Constants.ALL_DIAGONOL_LINE_MODE)
            {
                gameModeResults = Logic.ValidateDiagonalLineGameMode(gameMode, grid);
            }
            
            if (gameModeResults.GameWin)
            {
                gameWin = true;
                payoutRate = gameModeResults.Payout;
            }
            

            //Win or lose logic
            var gameWinCalc = Logic.CalculateGameWinDoubles(gameWin, wallet, wager, payoutRate);
            var gameWinStatus = UI.DisplayGameWinStatus(gameWin,  gameWinCalc.wallet, wager, gameWinCalc.payr );
            input = gameWinStatus.ContinueGame; // Set game status to determine if while loop continues 
            wallet = gameWinStatus.Wallet;
            //totalPayout = gameWinStatus.Payout;
        } 
    }
}