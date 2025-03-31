using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShip : MonoBehaviour
{
    private PlayerInput input;
    private Rigidbody2D rb;
    private Ship ship;

    private float lastShotTime = -Mathf.Infinity;

    public UpgradeManager upgradeManager;

    //public GameObject projectile;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        input.ActivateInput();
        rb = GetComponent<Rigidbody2D>();
        ship = GetComponent<Ship>();
        upgradeManager.SetPlayerShip(ship);
    }

    private void Start()
    {
        while (!ship.abilities.IsInitialized()) ;
        DecorateBase();

    }

    public void DecorateBase()
    {
        ShipAbilitiesDecorator deco = new QuitGameOnDeath(ship.abilities.getActiveAbilities());
        ship.abilities.AddDecorator(deco);
    }

    private void OnMove(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();
        rb.linearVelocity = input * ship.stats.moveSpeed;
    }

    private void OnAttack(InputValue input)
    {
        float cooldown = 1.5f / ship.stats.attackSpeed;

        if (Time.time - lastShotTime >= cooldown)
        {
            ship.abilities.Attack(ship.stats.projectilePrefab, transform, Vector2.up, ship.stats.additionalProjectileSpeed);
            lastShotTime = Time.time;
        }
        
    }

    private void OnAbility(InputValue input)
    {
        ship.abilities.Abilities();
    }

    private void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -10f, 10f), Mathf.Clamp(transform.position.y, -5f, 0f));
    }
}
