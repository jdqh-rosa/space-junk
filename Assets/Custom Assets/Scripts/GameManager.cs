using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//**// GAME MANAGER CODE //**//
//**// NFI //**//

[ExecuteInEditMode]
public class GameManager : MonoBehaviour
{
    public GameObject[] earths;
    public GameObject[] satellites;
    public GameObject[] earthBases;
    public GameObject[] rockets;
    public GameObject[] spaceDebris;
    public TrashRing trashRing;
    
    public bool init=false;

    public void Init()
    {
        trashRing = new TrashRing();
        init = true;
    }

    void Update()
    {

    }
}

public enum Phase
{
    Past,
    Present,
    Future
}
