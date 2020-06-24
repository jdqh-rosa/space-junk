using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject introRocket;

    [Header("Panels")]
    public GameObject canvas;
    public GameObject badShot;
    public GameObject explode;
    public GameObject explode2;
    public GameObject corShot;
    public GameObject corShot2;
    public GameObject corShot3;
    public GameObject netBut;
    public GameObject breakBut;
    public GameObject slowBut;
    public GameObject shieldBut;

    [Header("UI")]
    public GameObject[] score;
    public GameObject RubbleMeter;
    public GameObject[] buttons;

    private float pauseTime = 0.5f;
    private float timerT = 0;
    private float timer = 0;

    bool off;
    int step = 0;
    void Start()
    {
        GameManager.Instance.tutorialActive = true;
        //Helper.BowTo(score, -1);
        //RubbleMeter.SetActive(false);
        //Helper.BowTo(buttons, -1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!off)
        {
            switch (step)
            {
                case 0:
                    StartTut();
                    break;
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
        timer += GameManager.gameDeltaTime;
        timerT += Time.deltaTime;
    }

    private int slide = 0;

    void StartTut()
    {
        if (!introRocket.activeSelf)
        {
            ++step;
        }
    }

    private bool once;
    void FailedTut()
    {
        if (!once)
        {
            timerT = 0;
            timer = 0;
            PauseGame();
            badShot.SetActive(true);
            once = !once;
        }
        if (Input.GetKeyDown(GameManager.Instance.laserKey) && timerT > pauseTime)
        {
            GameManager.Instance.shoot = true;
            badShot.SetActive(false);
            StartGame();
        }

        if (timer > 1)
        {

            ++step;
            timer = 0;
            once = !once;
        }
        else if(timer>=0.05f)
        {
            GameManager.Instance.shoot = false;
        }
    }
    void MeterTut()
    {
        PauseGame();
        GameManager.Instance.shoot = false;
        switch (slide)
        {
            case 0:
                TimerTZero();
                explode.SetActive(true);
                explode2.SetActive(false);
                break;
            case 1:
                TimerTZero();
                explode.SetActive(false);
                explode2.SetActive(true);
                RubbleMeter.SetActive(true);
                break;
            case 2:
                explode.SetActive(false);
                explode2.SetActive(false);
                once=false;
                ++step;
                slide = 0;
                break;
        }

        if (Input.GetKeyDown(GameManager.Instance.laserKey) && timerT > pauseTime)
        {
            ++slide;
            TimerTZero();
        }
    }
    void CorrectTut()
    {
        PauseGame();

        if (!once)
        {
            GameManager.Instance.satelliteObject.transform.position = Helper.CalcDegToPos(Helper.CalcPosToDeg(GameManager.Instance.earthLocation, GameManager.Instance.baseObject.transform.position), GameManager.Instance.satelliteRadius);
            GameManager.Instance.satelliteObject.transform.rotation = Quaternion.Euler(0, 0, Helper.CalcPosToDeg(GameManager.Instance.earthLocation, GameManager.Instance.baseObject.transform.position) - 90);
            GameManager.Instance.shoot = true;
            once = !once;
        }

        switch (slide)
        {
            case 0:
                TimerTZero();
                corShot.SetActive(true);
                corShot2.SetActive(false);
                corShot3.SetActive(false);
                break;
            case 1:
                TimerTZero();
                corShot.SetActive(false);
                corShot2.SetActive(true);
                corShot3.SetActive(false);
                Helper.ToBow(score, -1);
                StartGame();
                GameManager.Instance.shoot = false;
                break;
            case 2:
                TimerTZero();
                corShot.SetActive(false);
                corShot2.SetActive(false);
                corShot3.SetActive(true);
                break;
            case 3:
                corShot.SetActive(false);
                corShot2.SetActive(false);
                corShot3.SetActive(false);
                ++step;
                once=false;
                slide = 0;
                break;
        }

        if (Input.GetKeyDown(GameManager.Instance.laserKey) && timerT>pauseTime)
        {
            ++slide;
        }


    }
    void NetTut()
    {
        TimerTZero();
        PauseGame();
        buttons[0].SetActive(true);
        netBut.SetActive(true);

        if (Input.GetKeyDown(GameManager.Instance.laserKey) && timerT>pauseTime)
        {
            ++step;
            netBut.SetActive(false);
            once=false;
        }
    }
    void BreakTut()
    {
        TimerTZero();
        PauseGame();
        buttons[1].SetActive(true);
        breakBut.SetActive(true);

        if (Input.GetKeyDown(GameManager.Instance.laserKey) && timerT>pauseTime)
        {
            ++step;
            breakBut.SetActive(false);
            once=false;
        }
    }
    void SlowTut()
    {
        TimerTZero();
        PauseGame();
        buttons[2].SetActive(true);
        slowBut.SetActive(true);

        if (Input.GetKeyDown(GameManager.Instance.laserKey) && timerT>pauseTime)
        {
            ++step;
            slowBut.SetActive(false);
            once=false;
        }
    }
    void HoldTut()
    {
        TimerTZero();
        PauseGame();
        buttons[3].SetActive(true);
        shieldBut.SetActive(true);

        if (Input.GetKeyDown(GameManager.Instance.laserKey) && timerT>pauseTime)
        {
            ++step;
            shieldBut.SetActive(false);
            once=false;
        }
    }

    void EndTut()
    {
        off = true;
        TrashHandler.ClearTrash();
        GameManager.Instance.actProgression = 0;
        GameManager.Instance.score = 0;
        GameManager.Instance.currentMultiplier = 1f;
        GameManager.Instance.tutorialActive = false;
        StartGame();
    }

    void PauseGame()
    {
        GameManager.gameTimeScale = 0;
        canvas.SetActive(true);

        if (Input.GetMouseButtonDown(0))
        {
            //StartGame();
            if (off) { gameObject.SetActive(false); }
        }
    }

    void StartGame()
    {
        GameManager.gameTimeScale = 1;
        //step += 1;
        //canvas.SetActive(false);
    }

    void TimerTZero()
    {
        if (!once)
        {
            timerT = 0;
            once = !once;
        }
    }
}
