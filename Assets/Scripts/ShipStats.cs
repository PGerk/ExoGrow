using UnityEngine;

public class ShipStats : MonoBehaviour
{
    public string shipName = "Ship";
    public float damage = 1f;
    public float attackSpeed = 1f;
    public bool canFire = true;
    public float additionalProjectileSpeed = 0f;
    public float maxShield = 10f;
    public float shield = 10f;
    public float maxArmor = 5f;
    public float armor = 5f;
    public float moveSpeed = 5f;
    public GameObject projectilePrefab;
}
