public interface IPlayerState
{
    void EnterState(CharacterMovement character);
    void UpdateState(CharacterMovement character);
    void ExitState(CharacterMovement character);
}