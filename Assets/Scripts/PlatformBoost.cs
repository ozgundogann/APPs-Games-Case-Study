using UnityEngine;

public class PlatformBoost : Platforms
{
    [SerializeField] private float forceAmount;

    public override void PlatformResponsibility(CharacterPhysics chPhysic)
    {
        ApplyForceVertically(chPhysic);
    }

    private void ApplyForceVertically(CharacterPhysics chPhysic)
    {
        chPhysic.Velocity = Vector3.zero;
        chPhysic.Velocity = new Vector3(chPhysic.Velocity.x, forceAmount, chPhysic.Velocity.z);
    }
}