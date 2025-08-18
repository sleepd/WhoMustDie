public class GameManagerStateMachine : StateMachine
{
    public LoadingState loadingState = new();
    public MainmenuState mainmenuState = new();
    public PlayingState playingState = new();

}
