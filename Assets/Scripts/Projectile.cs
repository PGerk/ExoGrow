using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed { get; set; } = 10f;
    public float damage { get; set; } = 10f;
    public float lifetime { get; set; } = .5f;
    public Vector2 direction { get; set; } = Vector2.up;
    public GameObject owner { get; set; }

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (lifetime <= 0)
        {
            Expire();
        }
        else
        {
            rb.linearVelocity = direction * speed;
            lifetime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == owner.tag) return;
        collision.gameObject.GetComponent<ShipAbilities>().DamageTaken(damage);
        Expire();
    }

    private void Expire()
    {
        if (owner)
        {
            owner.GetComponent<Ship>().abilities.ProjectileExpire(gameObject);
        }
        
        Destroy(gameObject);
    }
}
