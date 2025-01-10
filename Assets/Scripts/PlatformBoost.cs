using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.Serialization;

public class PlatformBoost : Platform
{
    [SerializeField] private float impulse;
    

    public override void PlatformResponsibility(CharacterMovement chMovement)
    {
        chMovement.PerformBoosterJump(impulse);
    }
}