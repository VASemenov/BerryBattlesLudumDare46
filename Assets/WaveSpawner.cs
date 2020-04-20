using System.Collections;
using System.Collections.Generic;
using References;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public IntReference timeLeft;
    public Army enemyArmy;
    public int waveNumber = 0;

    public Battalion[] availableBattalions;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft.Value = 45;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft.Value == 0)
        {
            Spawn();
            waveNumber++;
            timeLeft.Value = timeLeft.defaultValue;
        }
    }

    private void Spawn()
    {
        var battalionsToSpawn = waveNumber < 10 ? 2 + waveNumber : 12;
        for (var i = 0; i < battalionsToSpawn; i++)
        {
            enemyArmy.AddBattalion(availableBattalions[Random.Range(0, availableBattalions.Length)], true);
        }
        Debug.Log("Army " + enemyArmy + " " + enemyArmy.battalions.Count);
    }
}
