using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    List<GameObject> objectList;
    public float suckSpeed;
    void Start()
    {
        transform.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector2.zero).x, transform.position.y, transform.position.z);
        transform.position += Vector3.up * 16;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= Camera.main.ViewportToWorldPoint(Vector3.one).x)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "TrashJunk")
        {
            if (!objectList.Contains(collision.gameObject))
            {
                objectList.Add(collision.gameObject);
            }
        }
    }

    void Suction()
    {
        foreach (GameObject g in objectList)
        {
            if (g == null)
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
