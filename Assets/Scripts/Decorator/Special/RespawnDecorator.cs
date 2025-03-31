using UnityEngine;
using System.Collections;

public class RespawnDecorator : ShipAbilitiesDecorator
{
    private bool hasRespawned = false;
    private float respawnTime = 1f; // Dauer der Unsichtbarkeit nach dem "Tod"
    //private float fadeSpeed = 1f; // Geschwindigkeit, mit der das Schiff ein- und ausblendet

    public RespawnDecorator(ShipAbilitiesBase ability) : base(ability)
    {
        name = "Theseus Override";
        description = "Upon destruction, the ship reassembles from backup components—technically the same vessel, depending on your definition of 'self.'";
    }

    public override void OnDeath(GameObject gameObject)
    {
        if (!hasRespawned)
        {
            hasRespawned = true;
            Debug.Log("Cheating Death...");
            owner.StartCoroutine(RespawnSequence());
        }
        else
        {
            base.OnDeath(gameObject); // Falls schon respawned, normal sterben
        }
    }

    private IEnumerator RespawnSequence()
    {
        yield return owner.StartCoroutine(FadeOut());

        SetShipVisibility(false);

        owner.stats.armor = Mathf.Infinity;

        yield return new WaitForSeconds(respawnTime);

        yield return owner.StartCoroutine(FadeIn());

        owner.stats.armor = owner.stats.maxArmor;
        owner.stats.shield = owner.stats.maxShield;

        SetShipVisibility(true);
    }

    private IEnumerator FadeOut()
    {
        SpriteRenderer sr = owner.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color startColor = sr.color;
            float elapsedTime = 0f;

            while (elapsedTime < respawnTime)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / respawnTime); // Geht von 1 auf 0 (unsichtbar)
                sr.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
                yield return null;
            }

            sr.color = new Color(startColor.r, startColor.g, startColor.b, 0f); // Sicherstellen, dass es vollständig unsichtbar wird
        }
        Debug.Log("FadeOut");
    }

    private IEnumerator FadeIn()
    {
        SpriteRenderer sr = owner.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color startColor = sr.color;
            float elapsedTime = 0f;

            while (elapsedTime < respawnTime)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(0f, 1f, elapsedTime / respawnTime);
                sr.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
                yield return null;
            }

            sr.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
        }
        Debug.Log("FadeIn");
    }

    private void SetShipVisibility(bool visible)
    {
        SpriteRenderer sr = owner.GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = visible;

        Collider2D col = owner.GetComponent<Collider2D>();
        if (col != null) col.enabled = visible;
    }
}
