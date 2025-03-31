using UnityEngine;

public class BasicEnemyDecorator : ShipAbilitiesDecorator
{
    public BasicEnemyDecorator(ShipAbilitiesBase abilities) : base(abilities)
    {
        
    }

    public override void OnDeath(GameObject gameObject)
    {
        base.OnDeath(gameObject);
        WaveManager waveManager = GameObject.Find("Wave Manager").GetComponent<WaveManager>();
        waveManager.EnemyDown(gameObject);
    }
    
}
