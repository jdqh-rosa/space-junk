using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRocket : MonoBehaviour
{
    public void Launch()
    {
        GameObject[] rockets = GameManager.Instance.rockets[GameManager.Instance.act-1];
        GameObject rocketObject = Instantiate(rockets[Random.Range(0, rockets.Length-1)], transform.position, transform.rotation);
        SetRocketParams(rocketObject);
    }

    public void ImperviousLaunch()
    {
        GameObject[] rockets = GameManager.Instance.rockets[GameManager.Instance.act];
        GameObject impRocket = Instantiate(rockets[Random.Range(0, rockets.Length-1)], transform.position, transform.rotation);
        SetRocketParams(impRocket);
        impRocket.GetComponent<BoxCollider>().enabled= false;
    }

    void SetRocketParams(GameObject rocket)
    {
        if(rocket.GetComponent<Rocket>() == null) { rocket.AddComponent<Rocket>(); }
        rocket.GetComponent<Rocket>().launchDistance = GameManager.Instance.rocketLaunchHeight;
        rocket.GetComponent<Rocket>().launchDev = GameManager.Instance.rocketLaunchHeightDev;
        rocket.GetComponent<Rocket>().launchSpeed = GameManager.Instance.rocketLaunchSpeed;
        rocket.GetComponent<Rocket>().movementSpeed = GameManager.Instance.rocketFlightSpeed;
        rocket.GetComponent<Rocket>().destructTime = GameManager.Instance.rocketDestructTime;

        if (rocket.GetComponent<Rocket>().trashHub == null) { rocket.GetComponent<Rocket>().trashHub = GameManager.Instance.trashHub; }
        rocket.GetComponent<Rocket>().trashHub.GetComponent<JunkDrop>().trashPrefab = GameManager.Instance.spaceDebrisPrefabs[GameManager.Instance.act - 1];
    }
}
