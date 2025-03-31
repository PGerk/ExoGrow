using UnityEngine;

public class ArmorDecorator : ShipAbilitiesDecorator
{
    private bool hasAppliedBoost = false;
    public ArmorDecorator(ShipAbilitiesBase ability) : base(ability)
    {
        name = "Reactive Nanocomposite";
        description = "Self-organizing alloys reinforce hull integrity.";
    }

    public override void Initialize(Ship ship)
    {
        base.Initialize(ship);
        if (!hasAppliedBoost)
        {
            owner.stats.armor += 5f;
            Debug.Log(name + " received!");
            Debug.Log(description);
            Debug.Log("Armor is now " + owner.stats.armor);
            hasAppliedBoost = true;
        }
    }
}
