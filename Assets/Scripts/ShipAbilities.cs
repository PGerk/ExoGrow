using System.Collections.Generic;
using UnityEngine;

public class ShipAbilities : MonoBehaviour
{
    private ShipAbilitiesBase activeAbilities;
    [SerializeField] private ShipAbilitiesBase baseAbilities;
    private Ship ship;

    private List<ShipAbilitiesDecorator> decorators = new List<ShipAbilitiesDecorator>();

    private void Awake()
    {
        ship = GetComponent<Ship>();
    }

    private void Start()
    {
        if (baseAbilities == null)
        {
            baseAbilities = new EmptyDecorator();
        }

        activeAbilities = baseAbilities;
        activeAbilities.Initialize(ship);
    }

    public bool IsInitialized()
    {
        return (ship != null ? true : false);
    }

    public ShipAbilitiesBase getActiveAbilities()
    {
        return activeAbilities;
    }

    public void DamageTaken(float amount)
    {
        activeAbilities.OnDamageTaken(amount);
    }
    public void EnemyKilled(ShipStats enemy)
    {
        activeAbilities.OnEnemyKilled(enemy);
    }
    public void Update()
    {
        activeAbilities.OnUpdate();
    }

    public void Death()
    {
        activeAbilities.OnDeath(gameObject);
        Destroy(gameObject);
    }
    public void Abilities()
    {
        activeAbilities.OnAbilities();
    }

    public void ProjectileExpire (GameObject projectile)
    {
        activeAbilities.OnProjectileExpire(projectile);
    }
    public void Attack(GameObject projectile, Transform source, Vector2 direction, float additionalSpeed)
    {
        if (ship.stats.canFire)
        {
            GameObject shot = Instantiate(projectile, source.position, Quaternion.identity);
            Projectile pro = shot.GetComponent<Projectile>();
            pro.direction = direction;
            pro.speed += additionalSpeed;
            pro.damage += ship.stats.damage;
            pro.owner = gameObject;
            activeAbilities.OnAttack(pro);
        }

    }
    
    public void AddDecorator(ShipAbilitiesDecorator newAbility)
    {
        newAbility.SetWrappedAbility(activeAbilities);
        newAbility.Initialize(ship);
        decorators.Add(newAbility);
        activeAbilities = newAbility;
    }


    public void RemoveDecorator<T>() where T : ShipAbilitiesDecorator
    {
        for (int i = decorators.Count - 1; i >= 0; i--)
        {
            if (decorators[i] is T)
            {
                decorators.RemoveAt(i);
                RebuildAbilities();
                Debug.Log("Decorator entfernt: " + typeof(T).Name);
                return;
            }
        }
        Debug.Log("Decorator nicht gefunden: " + typeof(T).Name);
    }

    private void RebuildAbilities()
    {
        activeAbilities = baseAbilities;
        foreach (var decorator in decorators)
        {
            decorator.Initialize(ship);
            activeAbilities = decorator;
        }
    }
}