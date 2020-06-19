﻿using System.Collections;
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
            if (laserCountdown <= 0)
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
        laserCountdown -= GameManager.gameDeltaTime;
    }
}
