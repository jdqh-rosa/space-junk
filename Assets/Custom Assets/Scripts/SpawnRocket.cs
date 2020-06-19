using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRocket : MonoBehaviour
{
    GameObject rocketObject;
    private void Update()
    {
        //if (Input.GetKeyDown(GameManager.Instance.laserKey))
        //{
        //    if (GameManager.breakThroughActive)
        //    {
        //        ImperviousLaunch();
        //    }
        //    else if (GameManager.Instance.targetAcquired)
        //    {
        //        Launch();
        //    }
        //    else
        //    {
        //        FaultyLaunch();
        //    }
        //}
    }

    public void Launch()
    {
        GameObject[] rockets = GameManager.Instance.rockets[GameManager.Instance.act - 1];
        rocketObject = Instantiate(rockets[Random.Range(0, rockets.Length - 1)], transform.position, transform.rotation);
        SetRocketParams(rocketObject);
        Destroy(gameObject);
        GameManager.Instance.CreateBase();
    }
    public void FaultyLaunch()
    {
        Launch();
        rocketObject.GetComponent<Rocket>().faulty = true;
    }

    public void ImperviousLaunch()
    {
        Launch();
        rocketObject.GetComponent<BoxCollider>().enabled = false;
    }

    void SetRocketParams(GameObject rocket)
    {
        if (rocket.GetComponent<Rocket>() == null) { rocket.AddComponent<Rocket>(); }
        rocket.GetComponent<Rocket>().launchDistance = GameManager.Instance.rocketLaunchHeight;
        rocket.GetComponent<Rocket>().launchDev = GameManager.Instance.rocketLaunchHeightDev;
        rocket.GetComponent<Rocket>().launchSpeed = GameManager.Instance.rocketLaunchSpeed;
        rocket.GetComponent<Rocket>().movementSpeed = GameManager.Instance.rocketFlightSpeed;
        rocket.GetComponent<Rocket>().destructTime = GameManager.Instance.rocketDestructTime;

        if (rocket.GetComponent<Rocket>().trashHub == null) { rocket.GetComponent<Rocket>().trashHub = GameManager.Instance.trashHub; }
        //rocket.GetComponent<Rocket>().trashHub.GetComponent<JunkDrop>().trashPrefab = GameManager.Instance.spaceDebrisPrefabs[Random.Range(0, GameManager.Instance.spaceDebrisPrefabs.Length-1)];
    }
}
