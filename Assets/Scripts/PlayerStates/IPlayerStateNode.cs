public interface IPlayerStateNode
{
    void EnterState(PlayerManager playerManager);
    void UpdateState(PlayerManager playerManager);
    void ExitState(PlayerManager playerManager);
}