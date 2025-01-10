public interface IPlayerStateNode
{
    void EnterState(StateManager stateManager);
    void UpdateState(StateManager stateManager);
    void ExitState(StateManager stateManager);
}