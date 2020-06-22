using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleWhenDone : MonoBehaviour
{
    void Update()
    {
        if (GetComponentInChildren<ParticleSystem>().isStopped)
        {
            Destroy(gameObject);
        }
    }
}
