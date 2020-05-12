using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TrashCloud
{
    public int locatDeg;
    public float radius;
    public GameObject debris;
    public int range;
    public Vector2 relRange;
    public GameObject[] debrisCloud;
    public int cloudSize;
    void Start(){
        debrisCloud = new GameObject[cloudSize];
    }
}
