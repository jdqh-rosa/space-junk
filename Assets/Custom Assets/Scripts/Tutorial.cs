using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas;
    public GameObject[] panels;
    bool off;
    int step = 1;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGame();
            if (off) { gameObject.SetActive(false); }
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
            case 8:
                EndTut();
                break;
        }

    }
    void FailedTut()
    {
        PauseGame();
    }
    void MeterTut()
    {
        PauseGame();
    }
    void CorrectTut()
    {
        if (GameManager.Instance.targetAcquired)
        {
            PauseGame();
            GameManager.Instance.targetAcquired = false;
        }
    }
    void NetTut()
    {
        PauseGame();
    }
    void BreakTut()
    {
        PauseGame();
    }
    void SlowTut()
    {
        PauseGame();
    }
    void HoldTut()
    {
        PauseGame();
    }

    void EndTut()
    {
        off = true;
    }

    void PauseGame()
    {
        GameManager.gameTimeScale = 0;
        canvas.SetActive(true);
        Helper.BowTo(panels, step-1);
    }

    void StartGame()
    {
        GameManager.gameTimeScale = 1;
        step += 1;
        canvas.SetActive(false);
    }
}
