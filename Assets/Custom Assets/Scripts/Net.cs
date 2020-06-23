using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    public float movementSpeed;
    public float startSize;
    public float duration;

    private List<GameObject> objectList = new List<GameObject>();


    void Start()
    {
        transform.localScale = new Vector3(GameManager.Instance.netSize, 0.10f, GameManager.Instance.netSize);
    }

    float timer;
    void Update()
    {
        timer += GameManager.gameDeltaTime;

        if (timer <= duration && transform.localScale.x >= 0)
        {
            transform.localScale -= new Vector3(0.02f, 0, 0.02f);
            transform.position -= transform.up * movementSpeed;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TrashJunk")
        {
            Destroy(other.gameObject);
        }
    }
}
