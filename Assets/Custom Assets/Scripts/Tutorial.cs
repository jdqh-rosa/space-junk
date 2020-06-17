using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas;
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
        }

        if (GameManager.Instance.satelliteObject.GetComponent<Satellite>().targetAcquired)
        {
            PauseGame();
            GameManager.Instance.satelliteObject.GetComponent<Satellite>().targetAcquired = false;
        }
    }
    

    void PauseGame()
    {
        GameManager.gameTimeScale =0;
        canvas.SetActive(true);
    }

    void StartGame()
    {
        GameManager.gameTimeScale=1;
        canvas.SetActive(false);
    }
}
