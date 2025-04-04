using UnityEngine;

public class ProjectileSpeedDecorator : ShipAbilitiesDecorator
{
    private bool hasAppliedBoost = false;
    public ProjectileSpeedDecorator(ShipAbilitiesBase ability) : base(ability)
    {
        name = "Lorentz Accelerator";
        description = "Electromagnetic fields propel munitions at lethal velocities.";
    }

    public override void Initialize(Ship ship)
    {
        base.Initialize(ship);
        if (!hasAppliedBoost)
        {
            owner.stats.additionalProjectileSpeed += .2f;
            hasAppliedBoost = true;
        }
    }
}
