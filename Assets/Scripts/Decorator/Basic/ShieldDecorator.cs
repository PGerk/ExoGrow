using UnityEngine;

public class ShieldDecorator : ShipAbilitiesDecorator
{
    private bool hasAppliedBoost = false;
    public ShieldDecorator(ShipAbilitiesBase ability) : base(null)
    {
        name = "Shield Upgrade";
        description = "Upgrade the shield of your ship.";
    }

    public override void Initialize(Ship ship)
    {
        base.Initialize(ship);
        if (!hasAppliedBoost)
        {
            owner.stats.shield += 1f;
            Debug.Log(name + " received!");
            Debug.Log(description);
            Debug.Log("Shield is now " + owner.stats.damage);
            hasAppliedBoost = true;
        }
    }
}
