using UnityEngine;

public class AttackSpeedDecorator : ShipAbilitiesDecorator
{
    private bool hasAppliedBoost = false;
    public AttackSpeedDecorator(ShipAbilitiesBase ability) : base(ability)
    {
        name = "Hyperfire Regulator";
        description = "Optimized firing mechanisms reduce weapon cooldowns.";
    }

    public override void Initialize(Ship ship)
    {
        base.Initialize(ship);
        if (!hasAppliedBoost)
        {
            owner.stats.attackSpeed += .1f;
            Debug.Log(name + " received!");
            Debug.Log(description);
            Debug.Log("Attack speed is now " + owner.stats.attackSpeed);
            hasAppliedBoost = true;
        }
    }
}
