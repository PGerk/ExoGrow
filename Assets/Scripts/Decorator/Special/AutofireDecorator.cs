using UnityEngine;

public class AutofireDecorator : ShipAbilitiesDecorator
{
    //float lastShotTime = 0f;

    public AutofireDecorator(ShipAbilitiesBase ability) : base(ability)
    {
        isSpecial = true;
        name = "Volition-Linked Fire Control";
        description = "Weapon systems now operate in synchronous fire mode - maintain input to sustain continuous discharge.";
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Input.GetKey(KeyCode.Space))
        {
            owner.abilities.Attack(owner.stats.projectilePrefab, owner.transform, Vector2.up, owner.stats.additionalProjectileSpeed);
        }
    }
}
