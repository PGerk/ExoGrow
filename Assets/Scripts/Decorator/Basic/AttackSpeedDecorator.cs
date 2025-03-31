using UnityEngine;

public class AttackSpeedDecorator : ShipAbilitiesDecorator
{
    private bool hasAppliedBoost = false;
    public AttackSpeedDecorator(ShipAbilitiesBase ability) : base(null)
    {
        name = "Attack Speed Upgrade";
        description = "Upgrade your attack speed.";
    }

    public override void Initialize(Ship ship)
    {
        base.Initialize(ship);
        if (!hasAppliedBoost)
        {
            owner.stats.attackSpeed += .1f;
            Debug.Log(name + " received!");
            Debug.Log(description);
            Debug.Log("Attack speed is now " + owner.stats.damage);
            hasAppliedBoost = true;
        }
    }
}
