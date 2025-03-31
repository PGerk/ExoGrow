using UnityEngine;
using System.Collections;

public class ShieldConverter : ShipAbilitiesDecorator
{
    private bool isOnCooldown = false;
    private float cooldownDuration = 30f;

    public ShieldConverter(ShipAbilitiesBase ability) : base(ability)
    {
        isSpecial = true;
        name = "Oblivion Bypass Conduit";
        description = "Emergency protocol reroutes all shield energy into armor repair, converting 100% of remaining shields into instant hull restoration. (Active)";
    }

    public override void OnAbilities()
    {
        base.OnAbilities();

        if (isOnCooldown)
        {
            Debug.Log($"{name} is still on cooldown.");
            return;
        }

        owner.stats.armor += owner.stats.shield;
        owner.stats.shield = 0;

        owner.StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        isOnCooldown = true;
        Debug.Log($"{name} activated. Cooling down");
        yield return new WaitForSeconds(cooldownDuration);
        isOnCooldown = false;
        Debug.Log($"{name} is ready.");
    }
}
