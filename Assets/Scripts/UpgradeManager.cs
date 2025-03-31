using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public TextMeshProUGUI title1, title2, title3, description1, description2, description3;

    public List<ShipAbilitiesDecorator> standardDecorators = new List<ShipAbilitiesDecorator>();
    public List<ShipAbilitiesDecorator> specialDecorators = new List<ShipAbilitiesDecorator>();
    public List<ShipAbilitiesDecorator> acquiredUpgrades = new List<ShipAbilitiesDecorator>();
    private List<ShipAbilitiesDecorator> selectableDecorators = new List<ShipAbilitiesDecorator>();

    public float specialDecoratorChance = .2f;
    public Canvas canvasObject;
    public bool selectingUpgrade = false;

    private Ship playerShip;

    private void LoadDecorators()
    {
        List<ShipAbilitiesDecorator> loadedDecorators = new List<ShipAbilitiesDecorator>();

        ShipAbilitiesDecorator newDecorator = new DamageDecorator(null);
        loadedDecorators.Add(newDecorator);
        /*ShipAbilitiesDecorator newDecorator = new ShieldRecharge(null);
        loadedDecorators.Add(newDecorator);


        newDecorator = new DamageDecorator(null);
        loadedDecorators.Add(newDecorator);
        newDecorator = new ArmorDecorator(null);
        loadedDecorators.Add(newDecorator);
        newDecorator = new ShieldDecorator(null);
        loadedDecorators.Add(newDecorator);
        newDecorator = new AttackSpeedDecorator(null);
        loadedDecorators.Add(newDecorator);
        newDecorator = new MoveSpeedDecorator(null);
        loadedDecorators.Add(newDecorator);
        newDecorator = new ProjectileSpeedDecorator(null);
        loadedDecorators.Add(newDecorator);*/

        foreach (var decorator in loadedDecorators)
        {
            if (decorator.isSpecial)
            {
                // Falls ein `isSpecial`-Flag existiert
                specialDecorators.Add(decorator);
                Debug.Log("Added " + decorator.name + " to special decorators.");
            }

            else
            {
                standardDecorators.Add(decorator);
                Debug.Log("Added " + decorator.name + " to regular decorators.");
            }
            
        }

        newDecorator = new ShieldDecorator(null);
        selectableDecorators.Add(newDecorator);
        selectableDecorators.Add(newDecorator);
        selectableDecorators.Add(newDecorator);

    }

    public void Awake()
    {
        LoadDecorators();
        ToggleCanvasOff();
    }

    public void SetPlayerShip (Ship ship)
    {
        playerShip = ship;
        Debug.Log("Set Player Ship to " + ship.name);
    }
    
    public void ToggleCanvasOn()
    {
        canvasObject.enabled = true;
        selectingUpgrade = true;
        Time.timeScale = 0f;
    }

    public void ToggleCanvasOff()
    {
        canvasObject.enabled = false;
        selectingUpgrade = false;
        Time.timeScale = 1f;
    }

    public void SetUpgrades(ShipAbilitiesDecorator decorator1, ShipAbilitiesDecorator decorator2, ShipAbilitiesDecorator decorator3)
    {
        if (decorator1 == null || decorator2 == null || decorator3 == null)
        {
            Debug.LogError("Einer der Dekoratoren ist null!");
            return;
        }

        if (selectableDecorators.Count < 3)
        {
            selectableDecorators.Clear();
            selectableDecorators.Add(decorator1);
            selectableDecorators.Add(decorator2);
            selectableDecorators.Add(decorator3);
        }
        else
        {
            selectableDecorators[0] = decorator1;
            selectableDecorators[1] = decorator2;
            selectableDecorators[2] = decorator3;
        }

        title1.text = decorator1?.name ?? "N/A";
        description1.text = decorator1?.description ?? "N/A";

        title2.text = decorator2?.name ?? "N/A";
        description2.text = decorator2?.description ?? "N/A";

        title3.text = decorator3?.name ?? "N/A";
        description3.text = decorator3?.description ?? "N/A";
    }

    public ShipAbilitiesDecorator GenerateRandomDecorator()
    {
        bool isSpecial = Random.value < specialDecoratorChance;

        if (isSpecial && specialDecorators.Count > 0)
        {
            return GenerateRandomSpecialUpgrade();
        }
        else if (standardDecorators.Count > 0)
        {
            return GenerateRandomRegularDecorator();            
        }

        Debug.LogWarning("Keine Upgrades mehr verfügbar!");
        return null;
    }

    public ShipAbilitiesDecorator GenerateRandomRegularDecorator()
    {
        int index = Random.Range(0, standardDecorators.Count);
        ShipAbilitiesDecorator original = standardDecorators[index];

        // **Erzeuge eine neue Instanz des gleichen Typs!**
        return (ShipAbilitiesDecorator)System.Activator.CreateInstance(original.GetType(), (ShipAbilitiesBase)null);
    }


    public ShipAbilitiesDecorator GenerateRandomSpecialUpgrade()
    {
        int index = Random.Range(0, specialDecorators.Count);
        ShipAbilitiesDecorator original = specialDecorators[index];

        return (ShipAbilitiesDecorator)System.Activator.CreateInstance(original.GetType(), (ShipAbilitiesBase)null);
    }

    public void UpgradeSelected(int upgradeNumber)
    {
        if (!selectingUpgrade) return;

        ShipAbilitiesDecorator chosenUpgrade = null;

        if (upgradeNumber == 1)
        {
            chosenUpgrade = selectableDecorators[0];
        }
        else if (upgradeNumber == 2)
        {
            chosenUpgrade = selectableDecorators[1];
        }
        else if (upgradeNumber == 3)
        {
            chosenUpgrade = selectableDecorators[2];
        }
        else if (upgradeNumber == 0) // Skip
        {
            Debug.Log("Upgrade übersprungen.");
            ToggleCanvasOff();
            return;
        }

        if (chosenUpgrade != null)
        {
            playerShip.abilities.AddDecorator(chosenUpgrade);
            acquiredUpgrades.Add(chosenUpgrade);
            Debug.Log($"Upgrade {chosenUpgrade.name} hinzugefügt.");
        }

        ToggleCanvasOff();
    }

    public void ShowUpgradeSelection()
    {
        ShipAbilitiesDecorator upgrade1 = GenerateRandomDecorator();
        
        ShipAbilitiesDecorator upgrade2 = GenerateRandomDecorator();
        
        ShipAbilitiesDecorator upgrade3 = GenerateRandomDecorator();

        SetUpgrades(upgrade1, upgrade2, upgrade3);
        
        ToggleCanvasOn();
    }
}
