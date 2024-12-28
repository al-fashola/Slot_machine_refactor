namespace Slot_machine_refactor;

public class Logic
{
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
}