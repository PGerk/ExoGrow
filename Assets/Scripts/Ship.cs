using UnityEngine;

public class Ship : MonoBehaviour
{
    public ShipStats stats;
    public ShipAbilities abilities;

    public void Awake()
    {
        stats = GetComponent<ShipStats>();
        abilities = GetComponent<ShipAbilities>();
    }
}

