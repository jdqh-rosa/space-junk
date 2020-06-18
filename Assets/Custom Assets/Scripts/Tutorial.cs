using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas;
    bool off;
    void Start()
    {
        PauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGame();
            if (off) { gameObject.SetActive(false); }
        }

        if (GameManager.Instance.targetAcquired)
        {
             if (off) { return; }
            PauseGame();
            GameManager.Instance.targetAcquired = false;
            off = true;
        }
    }


    void PauseGame()
    {
        GameManager.gameTimeScale = 0;
        canvas.SetActive(true);
    }

    void StartGame()
    {
        GameManager.gameTimeScale = 1;
        canvas.SetActive(false);
    }
}
