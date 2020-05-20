using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [Header("Launch")]
    public float launchDuration;
    public float launchSpeed;
    public float launchDistance;
    [Header("Flight")]
    public float movementSpeed;
    public float destructTime;

    private Vector3 startPosition;
    private Vector3 endPosition;
    void Start()
    {
        startPosition = transform.position;
        endPosition = transform.position + transform.up * launchDistance;
    }

    void Update()
    {
        if (t <= launchDuration)
        {
            RocketLaunch();
        }
        else
        {
            RocketFlight();
        }
        if (0 >= destructTime) Destroy(gameObject);
        destructTime -= Time.deltaTime;
    }

    float t = 0;
    void RocketLaunch()
    {
        transform.position = Vector3.Slerp(startPosition, endPosition, t);
        t += Time.deltaTime;
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

}
