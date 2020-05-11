using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public GameObject laserPrefab;
    
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space)){
            Instantiate(laserPrefab);
        }
    }
}
