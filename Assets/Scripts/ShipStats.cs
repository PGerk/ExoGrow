using UnityEngine;

public class ShipStats : MonoBehaviour
{
    public string shipName = "Ship";

    public float damage = 1f;
    public float attackSpeed = 1f;
    public bool canFire = true;
    public float additionalProjectileSpeed = 0f;
    public float lastShotTime;

    public float maxShield = 10f;
    public float shield = 10f;

    public float maxArmor = 5f;
    public float armor = 5f;

    public float moveSpeed = 5f;

    public GameObject projectilePrefab;

    public void Awake()
    {
        lastShotTime = Time.time;
    }

    public void Update()
    {
        if (!canFire)
        {
            float cooldown = 1.5f / attackSpeed;

            if (Time.time - lastShotTime >= cooldown)
            {
                canFire = true;
                //lastShotTime = Time.time;
            }
        }
        
    }
}
