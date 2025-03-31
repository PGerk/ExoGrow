using UnityEngine;

public class MoveSpeedDecorator : ShipAbilitiesDecorator
{
    private bool hasAppliedBoost = false;
    public MoveSpeedDecorator(ShipAbilitiesBase ability) : base(null)
    {
        name = "Movement Speed Upgrade";
        description = "Upgrade your movement speed.";
    }

    public override void Initialize(Ship ship)
    {
        base.Initialize(ship);
        if (!hasAppliedBoost)
        {
            owner.stats.moveSpeed += .2f;
            Debug.Log(name + " received!");
            Debug.Log(description);
            Debug.Log("Movement Speed is now " + owner.stats.damage);
            hasAppliedBoost = true;
        }
    }
}
