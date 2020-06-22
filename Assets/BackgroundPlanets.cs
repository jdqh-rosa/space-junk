using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPlanets : MonoBehaviour
{
    int worldCount = 0;
    GameObject[] planets;

    private void Start()
    {
        //planets = GameManager.Instance.planetPrefabs;
        //SpawnWorldAll();
    }
    void Update()
    {
        if (GameManager.Instance.totalLaunches==GameManager.Instance.launchesForPlanet*worldCount)
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
        Instantiate(GameManager.Instance.planetPrefabs[worldCount], Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Random.Range(0, Screen.width), UnityEngine.Random.Range(0, Screen.height), 100)), Quaternion.identity);
    }
    void SpawnWorldAll()
    {
        for (int i = 0; i < planets.Length; ++i)
        {
            Instantiate(planets[i], Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Random.Range(0, Screen.width), UnityEngine.Random.Range(0, Screen.height), 100)), Quaternion.identity);
            Color color = planets[i].GetComponent<MeshRenderer>().sharedMaterial.color;
            color.a = 0;
            planets[i].GetComponent<MeshRenderer>().sharedMaterial.color = color;
        }
    }
}
