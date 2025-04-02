using UnityEngine;
using UnityEngine.UI;  // Wir verwenden UI-Elemente wie Slider
using TMPro;  // F�r TextMeshPro, falls du auch Text f�r den Fortschritt anzeigen m�chtest

public class PlayerStatsUI : MonoBehaviour
{
    public Slider shieldSlider;    // Slider f�r das Schild
    public Slider armorSlider;     // Slider f�r die R�stung
    public TextMeshProUGUI shieldText; // Optional: Text f�r den Schildwert
    public TextMeshProUGUI armorText;  // Optional: Text f�r den R�stungswert
    public Transform playerShipTransform;

    public Ship playerShip;  // Referenz zum Spieler-Schiff

    private void Start()
    {
        // Hole die Referenz zum Spieler-Schiff (kann je nach Setup variieren)
        //playerShip = FindObjectOfType<Ship>();

        if (playerShip == null)
        {
            Debug.LogError("Kein PlayerShip gefunden!");
        }

        // Initialisiere die Slider-Werte
        shieldSlider.maxValue = playerShip.stats.maxShield;  // Maximaler Schildwert
        armorSlider.maxValue = playerShip.stats.maxArmor;    // Maximaler R�stungswert

        SetSliderColor(shieldSlider, Color.blue); // Setzt die Farbe f�r den Schild-Slider auf Blau
        SetSliderColor(armorSlider, new Color(0.6f, 0.3f, 0.1f)); // Setzt die Farbe f�r den R�stungs-Slider auf Braun

        //AdjustSliderSize(shieldSlider);
        //AdjustSliderSize(armorSlider);
    }

    private void Update()
    {
        if (playerShip != null)
        {
            // Aktualisiere die Slider mit den aktuellen Werten
            shieldSlider.value = playerShip.stats.shield;
            armorSlider.value = playerShip.stats.armor;

            //UpdateSliderPosition();

            // Optional: Aktualisiere auch Textanzeigen f�r den Fortschritt
            if (shieldText != null)
            {
                shieldText.text = "Shield: " + playerShip.stats.shield.ToString("F1");
            }

            if (armorText != null)
            {
                armorText.text = "Armor: " + playerShip.stats.armor.ToString("F1");
            }
        }
    }


    private void SetSliderColor(Slider slider, Color color)
    {
        // �ndert die Farbe der F�llung des Sliders
        slider.fillRect.GetComponent<Image>().color = color;
    }

    private void UpdateSliderPosition()
    {
        // Positioniere die Slider basierend auf der Position des Schiffes
        Vector3 position = playerShipTransform.position;

        // Offset, damit die Slider unter dem Schiff sind
        position.y -= 1f; // Du kannst den Wert nach Bedarf anpassen

        // Positioniere die Slider im 3D-Raum
        shieldSlider.transform.position = position;
        armorSlider.transform.position = position;
    }

    private void AdjustSliderSize(Slider slider)
    {
        // Setze die Gr��e des RectTransforms der Slider
        RectTransform rectTransform = slider.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(160f, 20f);  // Hier kannst du die Gr��e der Slider anpassen
    }
}
