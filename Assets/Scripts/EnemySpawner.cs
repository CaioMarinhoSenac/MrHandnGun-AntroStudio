using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject[] spawnPoints;
    public int beatInterval = 2; // Spawna a cada 2 batidas

    private int beatCount = 0;

    private void OnEnable()
    {
        BeatManagerComAudio.OnBeat += OnBeat;
    }

    private void OnDisable()
    {
        BeatManagerComAudio.OnBeat -= OnBeat;
    }

    void OnBeat()
    {
        beatCount++;
        if (beatCount % beatInterval == 0)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0 || enemyPrefab == null) return;
        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
        Instantiate(enemyPrefab, spawn.position, spawn.rotation);
    }
}