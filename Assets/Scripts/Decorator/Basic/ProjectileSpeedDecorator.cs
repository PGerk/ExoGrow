using UnityEngine;

public class ProjectileSpeedDecorator : ShipAbilitiesDecorator
{
    private bool hasAppliedBoost = false;
    public ProjectileSpeedDecorator(ShipAbilitiesBase ability) : base(ability)
    {
        name = "Projectile Speed Upgrade";
        description = "Upgrade your projectile speed.";
    }

    public override void Initialize(Ship ship)
    {
        base.Initialize(ship);
        if (!hasAppliedBoost)
        {
            owner.stats.additionalProjectileSpeed += .2f;
            Debug.Log(name + " received!");
            Debug.Log(description);
            Debug.Log("Projectile speed is now " + owner.stats.additionalProjectileSpeed);
            hasAppliedBoost = true;
        }
    }
}
