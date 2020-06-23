using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float destructTime;
    void Update()
    {
        Destroy(gameObject, destructTime);
    }
}
