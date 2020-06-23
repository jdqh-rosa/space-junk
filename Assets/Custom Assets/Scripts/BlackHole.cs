using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [Header("Spawn Location")]
    public float radius;
    [Header("Spawn Duration")]
    public float startDuration = 5;
    public float middleDuration = 5;
    public float endDuration = 5;
    [Header("Black Hole")]
    public float maxSize;
    public float growSpeed;
    public float suckSpeed;

    private List<GameObject> objectList = new List<GameObject>();


    void SpawnBlackHole()
    {
        radius = GameManager.Instance.blackHoleRadius;
        transform.position = Helper.CalcDegToPos(Random.Range(0, 360), radius);
    }

    void Activation()
    {
        print("A");
        if (transform.localScale.y <= maxSize)
        {
            transform.localScale += new Vector3(growSpeed, growSpeed, growSpeed);
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
            transform.localScale -= new Vector3(growSpeed, growSpeed, growSpeed);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SpawnBlackHole();
    }

    float timer;
    void Update()
    {
        timer += GameManager.gameDeltaTime;

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
        else
        {
            Destroy(gameObject);
        }

        BlackHoleTouch();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TrashJunk")
        {
            //if (!objectList.Contains(other.gameObject))
            //{
                objectList.Add(other.gameObject);
            //}
        }
    }

    void BlackHoleTouch()
    {
        foreach (GameObject g in objectList)
        {
            if(g == null)
            {
                objectList.Remove(g);
                continue;
            }
            if ((g.transform.position - transform.position).magnitude <= 0.1f)
            {
                objectList.Remove(g);
            }
            g.GetComponent<Orbit>().enabled = false;
            Vector3 direction = (transform.position - g.transform.position).normalized;
            g.transform.position += direction * suckSpeed;
            if (g.transform.localScale.y > 0)
            {
                g.transform.localScale -= Vector3.one * 0.1f;
            }
            Destroy(g, 2);
        }
    }
}
