using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakthroughShot : MonoBehaviour
{
    void Update()
    {
        transform.position += transform.up *1f;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "TrashJunk")
        {
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.tag == "Base")
        {
            collider.gameObject.GetComponent<SpawnRocket>().ImperviousLaunch();
            Destroy(gameObject);
        }else if (collider.gameObject.tag == "Finish")
        {
            Destroy(gameObject);
        }
    }
}
