using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    public abstract void PlatformResponsibility(CharacterMovement chMovement);
}