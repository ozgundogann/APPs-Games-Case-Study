using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.Serialization;

public class PlatformBoost : Platforms
{
    [SerializeField] private float upwardForce;
    [SerializeField] private float forwardForce;
    

    public override void PlatformResponsibility(CharacterPhysics chPhysic)
    {
        ApplyForceVertically(chPhysic);
    }

    private void ApplyForceVertically(CharacterPhysics chPhysic)
    {
        chPhysic.Velocity = new Vector3(chPhysic.Velocity.x, upwardForce, forwardForce);
    }
}