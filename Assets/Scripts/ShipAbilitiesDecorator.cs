using UnityEngine;

public abstract class ShipAbilitiesDecorator : ShipAbilitiesBase
{
    protected ShipAbilitiesBase wrappedAbility;

    public string name = "";
    public string description = "";

    private bool initialized = false;

    public ShipAbilitiesDecorator(ShipAbilitiesBase ability)
    {
        if (ability == this)
        {
            Debug.LogError("Decorator cannot wrap itself!");
            return;
        }
        wrappedAbility = ability;
    }

    public void SetWrappedAbility(ShipAbilitiesBase ability)
    {
        if (ability == this)
        {
            Debug.LogError("Decorator cannot wrap itself!");
            return;
        }

        wrappedAbility = ability;

        if (owner != null && wrappedAbility != null)
        {
            wrappedAbility.Initialize(owner);
        }
    }

    private bool IsRecursiveCall()
    {
        ShipAbilitiesBase current = wrappedAbility;
        while (current is ShipAbilitiesDecorator decorator)
        {
            if (decorator == this)
            {
                return true; // Verhindert Endlosrekursion
            }
            current = decorator.wrappedAbility;
        }
        return false;
    }

    public override void OnDamageTaken(float amount)
    {
        if (wrappedAbility != null && !IsRecursiveCall())
        {
            wrappedAbility.OnDamageTaken(amount);
        }
    }

    public override void OnEnemyKilled(ShipStats enemy)
    {
        if (wrappedAbility != null && !IsRecursiveCall())
        {
            wrappedAbility.OnEnemyKilled(enemy);
        }
    }

    public override void OnUpdate()
    {
        if (wrappedAbility != null && !IsRecursiveCall())
        {
            wrappedAbility.OnUpdate();
        }
    }

    public override void OnDeath(GameObject gameObject)
    {
        if (wrappedAbility != null && !IsRecursiveCall())
        {
            wrappedAbility.OnDeath(gameObject);
        }
    }

    public override void OnAbilities()
    {
        if (wrappedAbility != null && !IsRecursiveCall())
        {
            wrappedAbility.OnAbilities();
        }
    }

    public override void OnAttack(Projectile projectile)
    {
        if (wrappedAbility != null && !IsRecursiveCall())
        {
            wrappedAbility.OnAttack(projectile);
        }
    }

    public override void OnProjectileExpire(GameObject projectile)
    {
        if (wrappedAbility != null && !IsRecursiveCall())
        {
            wrappedAbility.OnProjectileExpire(projectile);
        }
    }

    public override void Initialize(Ship ship)
    {
        if (initialized) return; // Verhindert doppelte Initialisierung

        base.Initialize(ship); // Falls nötig
        wrappedAbility?.Initialize(ship);

        initialized = true; // Markiere als initialisiert
    }
}
