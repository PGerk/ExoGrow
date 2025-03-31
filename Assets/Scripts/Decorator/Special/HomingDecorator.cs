using UnityEngine;

public class HomingDecorator : ShipAbilitiesDecorator
{
    private float homingStrength = 2.5f; // Stärke der Lenkung

    public HomingDecorator(ShipAbilitiesBase ability) : base(ability)
    {
        name = "Eigenvector Superposition Rounds";
        description = "Bullets exist in a probabilistic eigenstate until impact, collapsing their wavefunction onto the target’s most vulnerable trajectory eigenplane.";
    }

    public override void OnAttack(Projectile projectile)
    {
        base.OnAttack(projectile);

        HomingProjectile homing = projectile.gameObject.AddComponent<HomingProjectile>();
        homing.homingStrength = homingStrength;
    }
}
