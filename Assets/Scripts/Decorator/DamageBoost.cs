using UnityEngine;

public class DamageBoost : ShipAbilitiesDecorator
{
    public DamageBoost(ShipAbilitiesBase ability) : base(ability)
    {
    }

    public override void OnAttack(Projectile projectile)
    {
        Debug.Log("Damage Boost atkiviert!");
        owner.stats.damage += 1;
        owner.abilities.RemoveDecorator<DamageBoost>();
        base.OnAttack(projectile);
    }

}
