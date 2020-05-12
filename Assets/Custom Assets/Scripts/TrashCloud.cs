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

//initialize the cloud objects in an array
    public void CreateDebrisCloud(){
        debrisCloud = new GameObject[cloudSize];
        for(int i=0; i< cloudSize; ++i){
            //Vector2 blah = Helper.CalculateDegPos(relRange.x + (range/(cloudSize-1))*i, radius);
            debrisCloud[i] = debris as GameObject;
        }
    }

    //Update the position of every cloud object
    public void UpdatePos()
    {
        for(int i=0; i< cloudSize; ++i){
            Vector2 blah = Helper.CalculateDegPos(relRange.x + (range/(cloudSize-1))*i, radius);
            debrisCloud[i].transform.position = new Vector3(blah.x, blah.y, 0);
        }
    }
}
