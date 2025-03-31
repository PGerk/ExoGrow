using UnityEngine;

public class ShieldOverlay : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Ship ship;
    private float flashDuration = 0.1f; // Dauer des Flash-Effekts
    private Color originalColor;
    private bool isFlashing;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ship = GetComponentInParent<Ship>(); // Holt automatisch das Schiff-Objekt

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }

        UpdateShieldOpacity();
    }

    private void Update()
    {
        UpdateShieldOpacity();
    }

    private void UpdateShieldOpacity()
    {
        if (ship == null || ship.stats == null) return; // Falls das Schiff noch nicht initialisiert ist

        float shieldPercentage = ship.stats.shield / (float)ship.stats.maxShield;

        if (ship.stats.shield <= 0)
        {
            spriteRenderer.enabled = false; // Schild ausblenden, wenn es zerstört ist
        }
        else
        {
            spriteRenderer.enabled = true;
            if (!isFlashing) // Normaler Farbverlauf nur, wenn kein Flash aktiv ist
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 0.5f, shieldPercentage));
            }
        }
    }

    public void TriggerShieldFlash()
    {
        if (!isFlashing)
        {
            StartCoroutine(FlashEffect());
        }
    }

    private System.Collections.IEnumerator FlashEffect()
    {
        isFlashing = true;
        spriteRenderer.color = Color.white; // Schild kurz komplett weiß färben
        yield return new WaitForSeconds(flashDuration);
        isFlashing = false;
        UpdateShieldOpacity(); // Zurück zur normalen Transparenz
    }
}
