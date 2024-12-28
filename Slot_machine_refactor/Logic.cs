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
}