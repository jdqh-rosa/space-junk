using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRocket : MonoBehaviour
{
    public GameObject rocketPrefab;

    public void Launch()
    {
        Instantiate(rocketPrefab, transform.position, transform.rotation);
    }
}
