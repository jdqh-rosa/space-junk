using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickInteraction : MonoBehaviour
{
    public float laserRate = 1;
    float laserCountdown;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(GameManager.Instance.laserKey))
        {
            //Instantiate(GameManager.Instance.rippleEffect, Input.mousePosition, Quaternion.identity);

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "click")
                {
                    if (GameManager.Instance.tutorialActive)
                    {
                        if (GameManager.Instance.shoot)
                        {
                            LaunchRocket();
                        }
                    }
                    else if (laserCountdown <= 0)
                    {
                        LaunchRocket();
                    }
                }
            }
        }
        laserCountdown -= GameManager.gameDeltaTime;
    }

    void LaunchRocket()
    {
        if (GameManager.breakThroughActive)
        {
            GameManager.Instance.baseObject.GetComponent<SpawnRocket>().ImperviousLaunch();
        }
        else if (GameManager.Instance.targetAcquired)
        {
            GameManager.Instance.baseObject.GetComponent<SpawnRocket>().Launch();
        }
        else
        {
            GameManager.Instance.baseObject.GetComponent<SpawnRocket>().FaultyLaunch();
        }
        laserCountdown = 1 / laserRate;
    }

}
