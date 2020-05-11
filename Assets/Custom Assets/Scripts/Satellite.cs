using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

     public GameObject laserPrefab;
    
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space)){
            Instantiate(laserPrefab);
        }
    }
}
