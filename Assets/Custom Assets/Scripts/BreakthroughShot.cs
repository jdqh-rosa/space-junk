using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakthroughShot : MonoBehaviour
{
    void Update()
    {
        transform.position += transform.up *0.8f;
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
            GameManager.Instance.BeamHit();
            Destroy(gameObject);
        }else if (collider.gameObject.tag == "Finish")
        {
            GameManager.Instance.BeamMissed();
            Destroy(gameObject);
        }
    }
}
