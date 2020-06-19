using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public AudioSource clickSound;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickSound.Play();
        }
    }
}
