using UnityEngine;

public abstract class ShipAbilitiesBase
{
    public Ship owner;
    public bool isSpecial = false;
    public virtual void OnDamageTaken(float amount)
    {
        float shieldAbsorbed = Mathf.Min(owner.stats.shield, amount);
        owner.stats.shield -= shieldAbsorbed;
        amount -= shieldAbsorbed;

        if (amount > 0)
        {
            owner.stats.armor -= amount;

            if (owner.stats.armor <= 0)
            {
                owner.abilities.Death();
            }
        }
    }
    public virtual void OnEnemyKilled(ShipStats enemy)
    {
        Debug.Log("Enemy " + enemy.name + " destroyed!");
    }
    public virtual void OnUpdate()
    {

    }

    public virtual void OnDeath(GameObject owner)
    {
        
    }
    public virtual void OnAbilities()
    {
        //Debug.Log("Abilities Base");
    }

    public virtual void OnAttack (Projectile projectile)
    {

    }

    public virtual void OnProjectileExpire(GameObject projectile)
    {

    }

    public virtual void Initialize(Ship ship)
    {
        owner = ship;
    }

}
