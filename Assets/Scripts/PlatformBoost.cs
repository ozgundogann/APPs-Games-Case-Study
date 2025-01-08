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
        chPhysic.velocity = new Vector3(chPhysic.velocity.x, forceAmount, chPhysic.velocity.z);
    }
}