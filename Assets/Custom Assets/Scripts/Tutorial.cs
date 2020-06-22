using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas;
    bool off;
    int step = 0;
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

        switch (step)
        {
            case 1:
                FailedTut();
                break;
            case 2:
                MeterTut();
                break;
            case 3:
                CorrectTut();
                break;
            case 4:
                NetTut();
                break;
            case 5:
                BreakTut();
                break;
            case 6:
                SlowTut();
                break;
            case 7:
                HoldTut();
                break;
        }

    }
    void FailedTut()
    {

    }
    void MeterTut()
    {

    }
    void CorrectTut()
    {

    }
    void NetTut()
    {

    }
    void BreakTut()
    {

    }
    void SlowTut()
    {

    }
    void HoldTut()
    {

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
