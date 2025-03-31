using UnityEngine;

public class ShieldRecharge : ShipAbilitiesDecorator
{
    
    public ShieldRecharge(ShipAbilitiesBase ability) : base(ability)
    {
        isSpecial = true;
        name = "Ouroboros Capacitor";
        description = "Shield energy recycles itself in a closed loop, regenerating automatically.";
    }

    public override void OnUpdate()
    {
        if (owner.stats.shield < owner.stats.maxShield) owner.stats.shield += 0.001f;
    }

}
