using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] mobs;
    [SerializeField] Transform[] spawnPoints;

    [SerializeField] Text waveCountText;
    [SerializeField] Text nextWaveCounter;
    [SerializeField] Text gameTimeText;

    int waveCount;

    int nextWaveTimer;

    float timer;


    void Start()
    {
        waveCount = 1;
        nextWaveTimer = 14;
        timer = nextWaveTimer;

        SpawnMobs(waveCount);
    }

    
    void Update()
    {
        timer = Mathf.Max(timer - Time.deltaTime, 0);

        waveCountText.text = "Wave " + waveCount.ToString("0");
        nextWaveCounter.text = "Next Wave in: " + timer.ToString("0.00");

        if(timer <= 0)
        {
            waveCount += 1;
            //nextWaveTimer += waveCount < 10 ? 1 : 2;
            SpawnMobs(waveCount);
            timer = nextWaveTimer;
        }
    }

    void SpawnMobs(int count)
    {
        for (int i = 0; i <= count; i++)
        {
            Instantiate(mobs[Random.Range(0, mobs.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        }
    }
}
