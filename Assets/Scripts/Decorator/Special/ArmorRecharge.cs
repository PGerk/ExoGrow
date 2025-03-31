using UnityEngine;

public class ArmorRecharge : ShipAbilitiesDecorator
{
    
    public ArmorRecharge(ShipAbilitiesBase ability) : base(ability)
    {
        isSpecial = true;
        name = "Von Neumann Regenerator";
        description = "Self-replicating nanites automatically repair hull damage, prioritizing breached sectors.";
    }

    public override void OnUpdate()
    {
        if (owner.stats.armor < owner.stats.maxArmor) owner.stats.armor += 0.0001f;
    }

}
