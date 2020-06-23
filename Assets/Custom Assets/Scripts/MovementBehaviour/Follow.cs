using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public float movementSpeed = 5;
    public GameObject target;
    public Vector3 targetLocation;

    void Update()
    {
        if(!target)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetLocation, movementSpeed * GameManager.gameDeltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, movementSpeed * GameManager.gameDeltaTime);
        }
    }
}
