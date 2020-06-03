using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    //public GameObject blackHoleObject;
    public float startDuration = 5;
    public float middleDuration = 5;
    public float endDuration = 5;
    public float maxSize;
    public float growSpeed;

    void Awake()
    {
        //blackHoleObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    }

    void Activation()
    {
        print("A");
        if (transform.localScale.y <= maxSize)
        {
            transform.localScale += new Vector3(growSpeed, growSpeed, 0);
        }
    }

    void Middle()
    {
        print("B");
    }
    void DeActivation()
    {
        print("C");
        if (transform.localScale.y >= 0)
        {
            transform.localScale -= new Vector3(growSpeed, growSpeed, 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    float timer;
    void Update()
    {
        timer += Time.deltaTime;

        if (startDuration >= timer && transform.localScale.y <= maxSize)
        {
            Activation();
        }
        else if (startDuration + middleDuration >= timer)
        {
            Middle();
        }
        else if (startDuration + middleDuration + endDuration >= timer && transform.localScale.y >= 0)
        {
            DeActivation();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("AAAAAAA");
        if (other.gameObject.tag == "TrashJunk")
        {
            other.gameObject.transform.position = Vector3.MoveTowards(other.gameObject.transform.position, gameObject.transform.position ,0);
        }
    }
}
