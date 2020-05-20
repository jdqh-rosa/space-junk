using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public GameObject satellite;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (satellite.GetComponent<Satellite>().rayCastHit.collider.gameObject != null)
        {
            if (satellite.GetComponent<Satellite>().rayCastHit.collider.gameObject.tag == "Base" && satellite.GetComponent<Satellite>().shootMe)
            {
                satellite.GetComponent<Satellite>().rayCastHit.collider.gameObject.GetComponent<SpawnRocket>().Launch();
                satellite.GetComponent<Satellite>().shootMe = false;
            }
        }
    }
}
