using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShip : MonoBehaviour
{
    private PlayerInput input;
    private Rigidbody2D rb;
    private Ship ship;

    private float lastShotTime = -Mathf.Infinity;

    public UpgradeManager upgradeManager;

    public Canvas pauseMenuCanvas;

    private Camera mainCamera;
    private Vector2 screenBounds;
    private float playerWidth;
    private float playerHeight;

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
        mainCamera = Camera.main;

        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        playerWidth = sr.bounds.extents.x;
        playerHeight = sr.bounds.extents.y;

        pauseMenuCanvas.enabled = false;
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

    private void OnPause()
    {
        if (!upgradeManager.selectingUpgrade)
        {
            if (!pauseMenuCanvas.enabled)
            {
                pauseMenuCanvas.enabled = true;
                Time.timeScale = 0f;
            }
            else
            {
                pauseMenuCanvas.enabled = false;
                Time.timeScale = 1f;
            }
        }
    }
    private void Update()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -screenBounds.x + playerWidth, screenBounds.x - playerWidth);
        pos.y = Mathf.Clamp(pos.y, -screenBounds.y + (playerHeight/2), 0);
        transform.position = pos;
    }
}
