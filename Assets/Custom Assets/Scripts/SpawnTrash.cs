using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrash : MonoBehaviour
{
    public int amount;
    public GameObject kidPrefab;
    void Start()
    {
        for(int i=0; i<amount; ++i){
        Instantiate(kidPrefab);
        }
    }
}