using UnityEngine;

public abstract class ShipAbilitiesDecorator : ShipAbilitiesBase
{
    protected ShipAbilitiesBase wrappedAbility;

    public string name = "";
    public string description = "";

    public ShipAbilitiesDecorator(ShipAbilitiesBase ability)
    {
        wrappedAbility = ability;
    }

    public void SetWrappedAbility(ShipAbilitiesBase ability)
    {
        wrappedAbility = ability;
        if (owner != null) wrappedAbility.Initialize(owner);
    }


    public override void OnDamageTaken(float amount)
    {
        wrappedAbility.OnDamageTaken(amount);
    }

    public override void OnEnemyKilled(ShipStats enemy)
    {
        wrappedAbility.OnEnemyKilled(enemy);
    }

    public override void OnUpdate()
    {
        wrappedAbility.OnUpdate();
    }

    public override void OnDeath(GameObject gameObject)
    {
        wrappedAbility.OnDeath(gameObject);
    }

    public override void OnAbilities()
    {
        wrappedAbility.OnAbilities();
    }

    public override void OnAttack(Projectile projectile)
    {
        wrappedAbility.OnAttack(projectile);
    }

    public override void OnProjectileExpire(GameObject projectile)
    {
        wrappedAbility.OnProjectileExpire(projectile);
    }

    public override void Initialize(Ship ship)
    {
        base.Initialize(ship);
        wrappedAbility.Initialize(ship);
    }
}
