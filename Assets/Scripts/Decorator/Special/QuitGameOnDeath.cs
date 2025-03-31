using UnityEngine;

public class QuitGameOnDeath : ShipAbilitiesDecorator
{
    
    public QuitGameOnDeath(ShipAbilitiesBase ability) : base(ability)
    {
        isSpecial = true;
        name = "Lose Ability";
        description = "Yep, this makes you lose the game.";
    }

    public override void OnDeath(GameObject gameObject)
    {
        base.OnDeath(gameObject);
        SceneManagerGlobal.Instance.LoadScene("Title Screen");
    }

}
