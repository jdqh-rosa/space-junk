using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
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

    private List<GameObject> panels;

    bool off;
    int step = 1;
    void Start()
    {
        GameManager.Instance.tutorialActive = true;
        Helper.BowTo(score, -1);
        RubbleMeter.SetActive(false);
        Helper.BowTo(buttons, -1);
    }

    // Update is called once per frame
    void Update()
    {
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

    private int slide = 0;

    private float timer = 0;
    private bool once;
    void FailedTut()
    {
        if (!once)
        {
            PauseGame();
            badShot.SetActive(true);
            GameManager.Instance.shoot = true;
            once = !once;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            badShot.SetActive(false);
            StartGame();
            GameManager.Instance.shoot = false;
        }

        timer += GameManager.gameDeltaTime;

        if (timer > 1)
        {
            ++step;
            timer = 0;
            once = !once;
        }
    }
    void MeterTut()
    {
        PauseGame();
        RubbleMeter.SetActive(true);
        GameManager.Instance.shoot = false;
        switch (slide)
        {
            case 0:
                explode.SetActive(true);
                explode2.SetActive(false);
                break;
            case 1:
                explode.SetActive(false);
                explode2.SetActive(true);
                break;
            case 2:
                explode.SetActive(false);
                explode2.SetActive(false);
                ++step;
                slide = 0;
                break;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ++slide;
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

                corShot.SetActive(true);
                corShot2.SetActive(false);
                corShot3.SetActive(false);
                break;
            case 1:
                corShot.SetActive(false);
                corShot2.SetActive(true);
                corShot3.SetActive(false);
                StartGame();
                GameManager.Instance.shoot = false;
                timer += GameManager.gameDeltaTime;
                break;
            case 2:
                corShot.SetActive(false);
                corShot2.SetActive(false);
                corShot3.SetActive(true);
                break;
            case 3:
                corShot.SetActive(false);
                corShot2.SetActive(false);
                corShot3.SetActive(false);
                Helper.ToBow(score, -1);
                ++step;
                slide = 0;
                break;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ++slide;
        }


    }
    void NetTut()
    {
        PauseGame();
        buttons[0].SetActive(true);
        netBut.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ++step;
            netBut.SetActive(false);
        }
    }
    void BreakTut()
    {
        PauseGame();
        buttons[2].SetActive(true);
        breakBut.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ++step;
            breakBut.SetActive(false);
        }
    }
    void SlowTut()
    {
        PauseGame();
        buttons[3].SetActive(true);
        slowBut.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ++step;
            slowBut.SetActive(false);
        }
    }
    void HoldTut()
    {
        PauseGame();
        buttons[1].SetActive(true);
        shieldBut.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ++step;
            shieldBut.SetActive(false);
        }
    }

    void EndTut()
    {
        off = true;
        TrashHandler.ClearTrash();
        GameManager.Instance.actProgression=0;
        GameManager.Instance.score=0;
        GameManager.Instance.currentMultiplier = 1f;
        GameManager.Instance.tutorialActive=false;
    }

    void PauseGame()
    {
        GameManager.gameTimeScale = 0;
        canvas.SetActive(true);

        if (Input.GetMouseButtonDown(0))
        {
            StartGame();
            if (off) { gameObject.SetActive(false); }
        }
    }

    void StartGame()
    {
        GameManager.gameTimeScale = 1;
        //step += 1;
        //canvas.SetActive(false);
    }
}
