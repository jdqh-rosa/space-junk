using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPlanets : MonoBehaviour
{
    int worldCount = 0;
    private void Start()
    {
        //planets = GameManager.Instance.planetPrefabs;
        //SpawnWorldAll();
    }
    void Update()
    {
        if (GameManager.Instance.totalLaunches == GameManager.Instance.launchesForPlanet * (worldCount + 1))
        {
            SpawnWorld();
            ++worldCount;

            if (worldCount > GameManager.Instance.planetPrefabs.Length)
            {
                Destroy(gameObject);
            }
        }
    }

    void SpawnWorld()
    {
        ++GameManager.Instance.planetsexplored;
        Instantiate(GameManager.Instance.planetPrefabs[worldCount], Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Random.Range(0, Screen.width), UnityEngine.Random.Range(0, Screen.height), 100)), Quaternion.identity);
    }
}
