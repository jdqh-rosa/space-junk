using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    private void OnCollisionExit(Collision collision)
    {
        if (GameManager.breakThroughActive)
        {
            if(collision.gameObject.tag == "TrashJunk")
            {
                Destroy(collision.gameObject);
            }else if(collision.gameObject.tag == "Base")
            {
                collision.gameObject.GetComponent<SpawnRocket>().ImperviousLaunch();
            }
        }
    }
}
