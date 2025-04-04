using UnityEngine;
using System.Collections.Generic;
public class DamageDecorator : ShipAbilitiesDecorator
{
    private bool hasAppliedBoost = false;
    //private List<string> names = new List<string>();
    public DamageDecorator(ShipAbilitiesBase ability) : base(ability)
    {
        //names.Add("")
        name = "Overcharged Plasma Channels";
        description = "Weapons tap into unstable energy streams, amplifying destructive output.";
    }

    public override void Initialize(Ship ship)
    {
        base.Initialize(ship);
        if (!hasAppliedBoost)
        {
            owner.stats.damage += 0.2f;
            Debug.Log(name + " received!");
            Debug.Log(description);
            Debug.Log("Damage is now " + owner.stats.damage);
            hasAppliedBoost = true;
        }
    }
}
