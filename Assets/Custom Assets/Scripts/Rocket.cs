using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [Header("Launch")]
    //public float launchDuration;
    public float launchSpeed;
    public float launchDistance;
    public float launchRand;
    [Header("Flight")]
    public float movementSpeed;
    public float destructTime;

    private Vector3 startPosition;
   // private Vector3 endPosition;

    public bool destroy;
    void Start()
    {
        launchDistance = Random.Range(launchDistance - launchRand, launchDistance + launchRand+1);
        startPosition = transform.position;
        //endPosition = transform.position + transform.up * launchDistance;
    }

    void Update()
    {
        if (launchDistance >= (transform.position-startPosition).magnitude)
        {
            RocketLaunch();
        }
        else
        {
            RocketFlight();
        }
        if (0 >= destructTime && destroy) Destroy(gameObject);
        destructTime -= Time.deltaTime;
    }

    float t = 0;
    void RocketLaunch()
    {
        //transform.position = Vector3.Slerp(startPosition, endPosition, t);
        //t += Time.deltaTime * launchDuration/launchSpeed;
        transform.position += transform.up * launchSpeed * Time.deltaTime;

        if(launchDistance <= (transform.position-startPosition).magnitude)
        {
            DropJunk();
        }
    }

    void RocketFlight()
    {
        transform.position += transform.up * movementSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TrashJunk") || other.gameObject.CompareTag("TrashHub"))
        {
            Destroy(gameObject);
        }
    }

    void DropJunk()
    {
        GetComponent<JunkDrop>().Drop();
    }
}
