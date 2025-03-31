using UnityEngine;

public class DestroyOnDeath : ShipAbilitiesDecorator
{

    public DestroyOnDeath(ShipAbilitiesBase ability) : base(ability)
    {
        isSpecial = true;
        name = "Destroy Game Object on death";
        description = "Destroys the game object on death.";
    }

    public override void OnDeath(GameObject gameObject)
    {
        base.OnDeath(gameObject);
        GameObject.Destroy(owner.gameObject);
    }

}
