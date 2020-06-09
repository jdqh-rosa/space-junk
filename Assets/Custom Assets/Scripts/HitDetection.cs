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
        if (satellite.GetComponent<Satellite>().rayCastHit.collider != null)
        {
            if (satellite.GetComponent<Satellite>().shootMe)
            {
                if (satellite.GetComponent<Satellite>().rayCastHit.collider.gameObject.tag == "Base")
                {
                    //satellite.GetComponent<Satellite>().rayCastHit.collider.gameObject.GetComponent<SpawnRocket>().Launch();
                    
                    GameManager.Instance.currentStreak += 1;
                    print(GameManager.Instance.currentStreak);
                    if (baseSwitch)
                    {
                        Destroy(satellite.GetComponent<Satellite>().rayCastHit.collider.gameObject);
                        GameManager.Instance.CreateBase();
                    }
                }
                else
                {
                    if (!GameManager.Instance.holdStreak) GameManager.Instance.currentStreak = 0;
                }
                satellite.GetComponent<Satellite>().shootMe = false;
            }
        }
    }
}
