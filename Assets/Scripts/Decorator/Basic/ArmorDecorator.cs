using UnityEngine;

public class ArmorDecorator : ShipAbilitiesDecorator
{
    private bool hasAppliedBoost = false;
    public ArmorDecorator(ShipAbilitiesBase ability) : base(null)
    {
        name = "Armor Upgrade";
        description = "Upgrade the armor of your ship.";
    }

    public override void Initialize(Ship ship)
    {
        base.Initialize(ship);
        if (!hasAppliedBoost)
        {
            owner.stats.armor += 5f;
            Debug.Log(name + " received!");
            Debug.Log(description);
            Debug.Log("Armor is now " + owner.stats.damage);
            hasAppliedBoost = true;
        }
    }
}
