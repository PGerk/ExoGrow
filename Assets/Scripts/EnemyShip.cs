using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    private float changeDirectionTime = 3f;
    private float timeSinceLastChange = 0f;
    private float timeSinceLastShot = 0f;
    private Vector2 minBounds = new Vector2(-10, 0);
    private Vector2 maxBounds = new Vector2(10, 5);
    private Vector2 targetPosition;

    private Camera mainCamera;
    private Vector2 screenBounds;

    public Ship ship;

    //public WaveManager waveManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShipAbilitiesDecorator enemyBase = new BasicEnemyDecorator(ship.abilities.getActiveAbilities());
        ship.abilities.AddDecorator(enemyBase);
        enemyBase = new DestroyOnDeath(ship.abilities.getActiveAbilities());
        ship.abilities.AddDecorator(enemyBase);
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        minBounds.x = -screenBounds.x;
        maxBounds.x = screenBounds.x;
        minBounds.y = 0;
        maxBounds.y = screenBounds.y;
    }

    private void Awake()
    {
        ship = GetComponent<Ship>();
        SetNewTargetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        timeSinceLastChange += Time.deltaTime;

        if (timeSinceLastChange >= changeDirectionTime)
        {
            SetNewTargetPosition();
            timeSinceLastChange = 0f;
        }

        ship.transform.position = Vector2.MoveTowards(ship.transform.position, targetPosition, ship.stats.moveSpeed * Time.deltaTime);
    }

    private void SetNewTargetPosition()
    {
        float newX = Random.Range(minBounds.x, maxBounds.x);
        float newY = Random.Range(minBounds.y, maxBounds.y);
        targetPosition = new Vector2(newX, newY);
    }

    private void Shoot()
    {
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= (1.5f/ship.stats.attackSpeed))
        {
            FireProjectile();
            timeSinceLastShot = 0f;
        }
    }

    private void FireProjectile()
    {
        if (ship.stats.canFire)
        {
            GameObject projectile = GameObject.Instantiate(ship.stats.projectilePrefab, ship.transform.position, Quaternion.identity);
            Projectile proj = projectile.GetComponent<Projectile>();
            proj.direction = Vector2.down;
            proj.speed = 5f;
            proj.owner = ship.gameObject;
        }
    }

}
