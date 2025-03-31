using UnityEngine;


public class EmptyDecorator : ShipAbilitiesBase
{
   /* public EmptyDecorator(ShipAbilitiesBase ability) : base(ability)
    {
        name = "Projectile Speed Upgrade";
        description = "Upgrade your projectile speed.";
    }*/
    public override void OnDamageTaken(float amount)
    {
        base.OnDamageTaken(amount);
    }
    public override void OnEnemyKilled(ShipStats enemy)
    {
        base.OnEnemyKilled(enemy);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    public override void OnDeath(GameObject owner)
    {
        base.OnDeath(owner);
    }
    public override void OnAbilities()
    {
        base.OnAbilities();
    }
    public override void OnAttack(Projectile projectile)
    {
        base.OnAttack(projectile);
    }
    public override void OnProjectileExpire(GameObject projectile)
    {
        base.OnProjectileExpire(projectile);
    }

}
