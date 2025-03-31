using UnityEngine;

public class ShieldDecorator : ShipAbilitiesDecorator
{
    private bool hasAppliedBoost = false;
    public ShieldDecorator(ShipAbilitiesBase ability) : base(ability)
    {
        name = "Quantum Barrier Matrix";
        description = "Phase-shifting particles enhance shield durability.";
    }

    public override void Initialize(Ship ship)
    {
        base.Initialize(ship);
        if (!hasAppliedBoost)
        {
            owner.stats.shield += 1f;
            Debug.Log(name + " received!");
            Debug.Log(description);
            Debug.Log("Shield is now " + owner.stats.shield);
            hasAppliedBoost = true;
        }
    }
}
