using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTutorial : MonoBehaviour
{
    public GameObject introRocket;
    public GameObject tutorialCanvas;
    void Start()
    {
        tutorialCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!introRocket.activeSelf)
        {
            tutorialCanvas.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
