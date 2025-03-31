using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public float homingStrength = 2.5f;
    private Projectile projectile;

    private void Start()
    {
        projectile = GetComponent<Projectile>();
    }

    private void FixedUpdate()
    {
        GameObject target = FindNearestEnemy();
        if (target != null)
        {
            Vector2 directionToTarget = ((Vector2)target.transform.position - (Vector2)transform.position).normalized;

            projectile.direction = Vector2.Lerp(projectile.direction, directionToTarget, homingStrength * Time.fixedDeltaTime).normalized;
        }
    }

    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = enemy;
            }
        }
        return nearest;
    }
}
