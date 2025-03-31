using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<Wave> waves = new List<Wave>();
    //private int maxWaveNumber;
    private int currentWave = 0;
    private bool waveActive = false;

    private Vector2 minBounds = new Vector2(-10, 0);
    private Vector2 maxBounds = new Vector2(10, 5);

    public TextMeshProUGUI waveText;
    public float waveTextDisplayTime = 3f;
    public float blinkSpeed = 0.2f;

    public UpgradeManager upgradeManager;

    public GameObject enemyPrefab;

    void Start()
    {
        upgradeManager = GameObject.Find("Upgrade Manager").GetComponent<UpgradeManager>();
        //maxWaveNumber = waves.Count - 1;
        waveText.gameObject.SetActive(false);
        GenerateNextWave();
        StartCoroutine(StartWaveWithWarning(currentWave));
    }

    void Update()
    {
        if (waveActive && waves[currentWave].enemiesRemaining == 0)
        {
            StartCoroutine(WaveEnd(currentWave));
        }
    }

    private IEnumerator StartWaveWithWarning(int waveNumber)
    {
        yield return StartCoroutine(DisplayWaveMessage("Wave " + (waveNumber+1) + " incoming!"));
        WaveStart(waveNumber);
    }

    private void WaveStart(int waveNumber)
    {
        /*if (waveNumber <= maxWaveNumber)
        {
            foreach (GameObject enemy in waves[waveNumber].enemies)
            {
                Vector3 position = RandomV3();
                GameObject instantiatedEnemy = Instantiate(enemy, position, Quaternion.identity);
                waves[waveNumber].enemiesRemaining++;
            }
            waveActive = true;
            //Debug.Log("Wave started with " + waves[currentWave].enemiesRemaining + " enemies in play!");
        }*/

        int upgradeCount = Mathf.RoundToInt((currentWave + 1) * 1.5f);

        foreach (GameObject enemy in waves[waveNumber].enemies)
        {
            Vector3 position = RandomV3();
            GameObject instantiatedEnemy = Instantiate(enemy, position, Quaternion.identity);
            ShipAbilities enemyAbilities = instantiatedEnemy.GetComponent<ShipAbilities>();

            for (int i = 0; i < upgradeCount; i++)
            {
                enemyAbilities.AddDecorator(upgradeManager.GenerateRandomDecorator());
            }
            waves[waveNumber].enemiesRemaining++;
        }
        waveActive = true;
    }
    
    public void EnemyDown(GameObject enemy)
    {
        waves[currentWave].enemiesRemaining--;
        //Debug.Log("Enemy " + enemy + " down!");
        //Debug.Log(waves[currentWave].enemiesRemaining + " enemies remaining.");
        waves[currentWave].enemies.Remove(enemy);
    }

    private void GenerateNextWave()
    {
        Wave temp = new Wave();
        int enemyCount = Mathf.RoundToInt((currentWave+1) * 1.25f);

        for (int i = 0; i < enemyCount; i++)
        {
            temp.enemies.Add(enemyPrefab);
        }

        waves.Add(temp);
    }

    private IEnumerator WaveEnd(int waveNumber)
    {
        Debug.Log("Wave over");
        waveActive = false;
        yield return StartCoroutine(DisplayWaveMessage("Wave " + (currentWave+1) + " defeated!"));
        upgradeManager.ShowUpgradeSelection();
        currentWave++;

        GenerateNextWave();

        StartCoroutine(StartWaveWithWarning(currentWave));
    }

    private Vector3 RandomV3()
    {
        float newX = Random.Range(minBounds.x, maxBounds.x);
        float newY = Random.Range(minBounds.y, maxBounds.y);
        return new Vector3(newX, newY, 1);
    }

    private IEnumerator DisplayWaveMessage(string message)
    {
        waveText.text = message;
        waveText.gameObject.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < waveTextDisplayTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.PingPong(Time.time * 2f, 1f);
            waveText.alpha = alpha;
            yield return null;
        }

        waveText.gameObject.SetActive(false);
    }
}

[System.Serializable]
public class Wave
{
    public List<GameObject> enemies = new List<GameObject>();
    public int enemiesRemaining = 0;
}