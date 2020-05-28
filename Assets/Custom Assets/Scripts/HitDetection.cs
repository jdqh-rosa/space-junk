using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public GameObject satellite;
    public GameObject baseObject;
    public float worldRadius;
    public bool baseSwitch = false;
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

                if (baseSwitch)
                {
                    Destroy(satellite.GetComponent<Satellite>().rayCastHit.collider.gameObject);
                    CreateBase();
                }
            }
        }
    }

    void CreateBase()
    {
        int rand = Random.Range(0, 360);
        Instantiate(baseObject, Helper.CalcDegToPos(rand, worldRadius), Quaternion.Euler(0, 0, rand - 90));
    }

}
