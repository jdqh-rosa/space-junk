using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip[] mainMusic;
    public AudioClip[] transitionSound;

    int previousAct=0;
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = mainMusic[GameManager.Instance.act-1];
        audioSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if(previousAct != GameManager.Instance.act)
        {
            audioSource.clip = mainMusic[GameManager.Instance.act-1];
            audioSource.Play();
        }

        previousAct = GameManager.Instance.act;
    }

}
