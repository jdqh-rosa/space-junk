using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    public float laserDuration = 1;
    public GameObject laser;

    private bool laserOn=false;
    void Start()
    {
        laser.SetActive(false);
    }
    void Update()
    {
        ShootLaser();
    }

    float laserTimer=0;
    void ShootLaser(){
        
        if(Input.GetKeyUp(KeyCode.Space))
        {
            laserTimer = laserDuration;
        }
        
        if(laserTimer>0)
        {
            laserTimer-=Time.deltaTime;
            laser.SetActive(true);
        }else{
            laser.SetActive(false);
        }
    }
}
