public interface IPlayerStateNode
{
    void EnterState(CharacterMovements characterMovements);
    void UpdateState(CharacterMovements characterMovements);
    void ExitState(CharacterMovements characterMovements);
}