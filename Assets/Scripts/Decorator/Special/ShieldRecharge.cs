using UnityEngine;

public class ShieldRecharge : ShipAbilitiesDecorator
{
    
    public ShieldRecharge(ShipAbilitiesBase ability) : base(ability)
    {
        isSpecial = true;
        name = "Shield recharger";
        description = "Automatically recharges the shield slowly";
    }

    public override void OnUpdate()
    {
        if (owner.stats.shield < owner.stats.maxShield) owner.stats.shield += 0.01f;
    }

}
