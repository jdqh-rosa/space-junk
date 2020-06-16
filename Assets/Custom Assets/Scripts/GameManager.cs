﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

//**// GAME MANAGER CODE //**//
//**// NFI //**//

//[ExecuteInEditMode]
public sealed class GameManager : MonoBehaviour
{
    static public float gameTime;
    static public float gameDeltaTime;
    public SceneManager sceneManager;

    [Header("UI")]
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI rubblePercentageText;
    public TextMeshProUGUI actProgressionText;
    public TextMeshProUGUI actProgressionLabel;
    public TextMeshProUGUI multiplierText;

    [Header("Prefabs")]
    public GameObject[] earthPrefabs;
    public GameObject[] satellitePrefabs;
    public GameObject[] earthBasePrefabs;
    public GameObject[] act1RocketPrefabs;
    public GameObject[] act2RocketPrefabs;
    public GameObject[] act3RocketPrefabs;
    public GameObject[] spaceDebrisPrefabs;
    public GameObject blackHole;
    public GameObject netObject;
    public GameObject trashHub;

    public GameObject[][] rockets = new GameObject[3][];

    [Header("Trash Handler")]
    public TrashHandler trashHandler;

    [Space(10, order = 0)]

    [Header("Variables", order = 1)]

    [HideInInspector] public int score = 0;
    [HideInInspector] public int points = 0;

    public int act = 1;
    public float actProgression = 0;
    public int[] actCompletion;
    public float currentMultiplier = 1f;
    public float multiplierIncrease = 0.1f;
    public int pointsPerRocket = 10;
    public int maxRubble = 50;

    [Header("Earth", order = 1)]
    public Vector3 earthLocation;
    public float worldRadius;
    public float earthRotation;

    //[Header("Base")]
    //public int baseAmount;
    //public Vector3 baseLocation;

    [Header("Satellite")]
    public float satelliteRadius;
    public float satelliteSpeed;

    [Header("Laser")]
    public KeyCode laserKey;
    public float laserFireRate;
    public float laserActiveLength;


    [Header("Rocket")]
    public float rocketLaunchHeight;
    public float rocketLaunchHeightDev;
    public float rocketLaunchSpeed;
    public float rocketFlightSpeed;
    public float rocketDestructTime;

    [Header("Trash")]
    public float trashSpeed;
    public int trashDropAmount;
    public int trashDropRand;
    public float trashGap;

    [HideInInspector]
    public int currentRubble = 0;

    [Header("Skills", order = 0)]

    [Header("Skill: Maintain Streak", order = 1)]
    public int holdStreakPointCost;
    public float holdStreakDuration;
    public bool holdStreak;

    [Header("Skill: Slow Satellite")]
    public int slowSatPointCost;
    public float slowSatDuration;
    public float slowSatSpeed;
    public float slowSatPercentage;
    public bool slowSatActive = false;

    [Header("Skill: Net")]
    public int netPointCost;
    public float netSpeed;
    public float netSize;
    public float netDuration;
    public bool netActive;

    [Header("Skill: Break Through")]
    public int breakThroughPointCost;
    public static bool breakThroughActive;

    [Header("Events", order = 0)]

    [Header("Black Hole", order = 1)]
    public float blackHoleChancePerMinute;
    public float blackHoleRadius;
    public float blackHoleDuration;

    [Header("Garbage Truck")]
    public float garbageTruckChancePerMinute;
    public float garbageTruckSpeed;

    [Header("GamePlay Blabla", order = 0)]
    public int streak;
    public int currentStreak;

    private static GameManager INSTANCE = new GameManager();

    GameObject earthObject;
    GameObject satelliteObject;
    GameObject baseObject;
    GameObject[] trashObjects;

    public static GameManager Instance
    {
        get
        {
            return INSTANCE;
        }
    }

    private void Awake()
    {
        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            INSTANCE = this;
        }
    }

    public void Start()
    {
        rockets[0] = act1RocketPrefabs ;
        rockets[1] = act2RocketPrefabs;
        rockets[2] = act3RocketPrefabs;
        Instance.CreateEarth();
        Instance.CreateBase();
        Instance.CreateSatellite();
        Instance.TrashHandlerVars();
    }

    /// <summary>
    /// Add score based on the set variables and current streak multiplier
    /// </summary>
    public void BeamHit()
    {
        //Add score and add multiplier
        score += (int)(pointsPerRocket * currentMultiplier);
        points += (int)(pointsPerRocket * currentMultiplier);
        currentMultiplier += multiplierIncrease;

        //modify act progression
        actProgression++;
        if (actCompletion.Length > act - 1)
        {
            float percentage = (float)actProgression / (float)actCompletion[act - 1];

            if (actProgression >= actCompletion[act - 1])
            {
                NextAct();
            }
            else
            {
                actProgressionText.SetText(percentage.ToString("0%"));
            }
        }
        else
        {
            actProgressionText.SetText("∞");
        }

        //also update the UI
        pointsText.SetText("" + score);
        multiplierText.SetText(currentMultiplier.ToString("0.0") + "x");
    }

    /// <summary>
    /// Progress onto the next age
    /// </summary>
    public void NextAct()
    {
        act++;
        actProgression = 0;
        actProgressionText.SetText(0f.ToString("0%"));
        actProgressionLabel.SetText("ACT " + act);

        NewSatellite();
    }

    /// <summary>
    /// Reset multiplier if keep streak skill is not active
    /// </summary>
    public void BeamMissed()
    {
        if (!holdStreak)
        {
            currentMultiplier = 1f;
            multiplierText.SetText(currentMultiplier.ToString("0.0") + "x");
        }
    }

    /// <summary>
    /// Alter the rubble meter when rubble is dropped
    /// </summary>
    /// <param name="amount">amount of rubble blocks</param>
    public void RubbleDropped(int amount)
    {
        currentRubble = TrashHandler.ListCount();
        float percentage = (float)currentRubble / (float)maxRubble;
        rubblePercentageText.SetText(percentage.ToString("0%"));

        if (currentRubble >= maxRubble)
        {
            GameOver();
        }
    }

    /// <summary>
    /// Transition to the end screen and show the results
    /// </summary>
    public void GameOver()
    {
        PlayerPrefs.SetInt("score", score);
        sceneManager.LoadLevel(2);
    }

    public void CreateEarth()
    {
        earthObject = Instantiate(earthPrefabs[act - 1], earthLocation, Quaternion.identity);
    }
    public void CreateBase()
    {
        int rand = Random.Range(0, 360);
        baseObject = Instantiate(earthBasePrefabs[act - 1], Helper.CalcDegToPos(rand, worldRadius), Quaternion.Euler(0, 0, rand - 90));

        if (baseObject.GetComponent<SpawnRocket>() == null) { baseObject.AddComponent<SpawnRocket>(); }
        //baseObject.GetComponent<SpawnRocket>().rocketPrefab = act1RocketPrefabs[act - 1];
        //
        //if (baseObject.GetComponent<SpawnRocket>().rocketPrefab.GetComponent<Rocket>() == null) { baseObject.GetComponent<SpawnRocket>().rocketPrefab.AddComponent<Rocket>(); }
        //baseObject.GetComponent<SpawnRocket>().rocketPrefab.GetComponent<Rocket>().launchDistance = rocketLaunchHeight;
        //baseObject.GetComponent<SpawnRocket>().rocketPrefab.GetComponent<Rocket>().launchDev = rocketLaunchHeightDev;
        //baseObject.GetComponent<SpawnRocket>().rocketPrefab.GetComponent<Rocket>().launchSpeed = rocketLaunchSpeed;
        //baseObject.GetComponent<SpawnRocket>().rocketPrefab.GetComponent<Rocket>().movementSpeed = rocketFlightSpeed;
        //baseObject.GetComponent<SpawnRocket>().rocketPrefab.GetComponent<Rocket>().destructTime = rocketDestructTime;

        //if (baseObject.GetComponent<SpawnRocket>().rocketPrefab.GetComponent<Rocket>().trashHub == null) { baseObject.GetComponent<SpawnRocket>().rocketPrefab.GetComponent<Rocket>().trashHub = trashHub; }
        //baseObject.GetComponent<SpawnRocket>().rocketPrefab.GetComponent<Rocket>().trashHub.GetComponent<JunkDrop>().trashPrefab = spaceDebrisPrefabs[act - 1];

        baseObject.transform.parent = earthObject.transform;
    }

    public void CreateSatellite()
    {
        satelliteObject = Instantiate(satellitePrefabs[act - 1]);

        if (satelliteObject.GetComponent<Orbit>() == null) { satelliteObject.AddComponent<Orbit>(); }
        satelliteObject.GetComponent<Orbit>().radius = satelliteRadius;
        satelliteObject.GetComponent<Orbit>().orbitSpeed = satelliteSpeed;

        if (satelliteObject.GetComponent<Satellite>() == null) { satelliteObject.AddComponent<Satellite>(); }
        satelliteObject.GetComponent<Satellite>().target = earthObject;
        satelliteObject.GetComponent<Satellite>().laserRate = laserFireRate;
        satelliteObject.GetComponent<Satellite>().laserKey = laserKey;
        satelliteObject.GetComponent<Satellite>().laserDuration = laserActiveLength;

        if (satelliteObject.GetComponent<LineRenderer>() == null) { satelliteObject.AddComponent<LineRenderer>(); }
        satelliteObject.GetComponent<Satellite>().lr = satelliteObject.GetComponent<LineRenderer>();
    }

    public void NewSatellite()
    {
        GameObject tempSat = satellitePrefabs[act - 1];

        if (tempSat.GetComponent<Orbit>() == null) { tempSat.AddComponent<Orbit>(); }
        tempSat.GetComponent<Orbit>().radius = satelliteObject.GetComponent<Orbit>().radius;
        tempSat.GetComponent<Orbit>().orbitSpeed = satelliteObject.GetComponent<Orbit>().orbitSpeed;
        tempSat.GetComponent<Orbit>().objectDistanceAsRadius = true;

        if (tempSat.GetComponent<Satellite>() == null) { tempSat.AddComponent<Satellite>(); }
        tempSat.GetComponent<Satellite>().target = satelliteObject.GetComponent<Satellite>().target;
        tempSat.GetComponent<Satellite>().laserRate = satelliteObject.GetComponent<Satellite>().laserRate;
        tempSat.GetComponent<Satellite>().laserKey = satelliteObject.GetComponent<Satellite>().laserKey;
        tempSat.GetComponent<Satellite>().laserDuration = satelliteObject.GetComponent<Satellite>().laserDuration;

        if (tempSat.GetComponent<LineRenderer>() == null) { tempSat.AddComponent<LineRenderer>(); }
        tempSat.GetComponent<Satellite>().lr = tempSat.GetComponent<LineRenderer>();

        tempSat.transform.position = satelliteObject.transform.position;
        tempSat.transform.rotation = satelliteObject.transform.rotation;

        Destroy(satelliteObject);
        satelliteObject = Instantiate(tempSat);
    }

    void Update()
    {
        gameTime += Time.deltaTime;
        gameDeltaTime = Time.deltaTime;

        StreakHoldTimer();
        SlowSatTimer();
        SpawnBlackHole();
        NothingButNet();
    }

    /// <summary>
    /// turns the skill off once a certain amount of time has been reached
    /// </summary>
    float holdStreakTimer;
    private void StreakHoldTimer()
    {
        if (holdStreak)
        {
            holdStreakTimer += GameManager.gameDeltaTime;

            if (holdStreakDuration <= holdStreakTimer)
            {
                holdStreak = false;
                holdStreakTimer = 0;
            }
        }
    }

    /// <summary>
    /// turns the skill off once a certain amount of time has been reached
    /// </summary>
    float slowSatTimer;
    private void SlowSatTimer()
    {
        if (slowSatActive)
        {
            slowSatTimer += GameManager.gameDeltaTime;
            if (slowSatDuration >= slowSatTimer)
            {
                satelliteObject.GetComponent<Orbit>().orbitSpeed = slowSatSpeed;
            }
            else
            {
                satelliteObject.GetComponent<Orbit>().orbitSpeed = satelliteSpeed;
                slowSatActive = false;
                slowSatTimer = 0;
            }
        }
    }

    /// <summary>
    /// activates the skill when activated
    /// </summary>
    private void NothingButNet()
    {
        if (netActive)
        {
            GameObject net = Instantiate(netObject, satelliteObject.transform.position, satelliteObject.transform.rotation);
            if (net.GetComponent<Net>() == null) { net.AddComponent<Net>(); }
            net.GetComponent<Net>().duration = netDuration;
            net.GetComponent<Net>().movementSpeed = netSpeed;
            net.GetComponent<Net>().startSize = netSize;

            netActive = false;
        }
    }

    void TrashHandlerVars()
    {
        trashHandler.trashSpeedRand = trashDropRand;
        trashHandler.minGapLength = trashGap;
    }

    /// <summary>
    /// has a chance of spawning a black hole every minute
    /// </summary>
    float blackHoleTimer = 0;
    void SpawnBlackHole()
    {
        blackHoleTimer += GameManager.gameDeltaTime;
        if (blackHoleTimer >= 60)
        {
            if (Random.Range(0f, 1f) <= blackHoleChancePerMinute / 100f)
            {
                Instantiate(blackHole);
            }
            blackHoleTimer = 0;
        }
    }


    //**// Methods to switch skill bools//**//
    public void ActivateHoldStreak()
    {
        holdStreak = true;
        points -= holdStreakPointCost;
    }
    public void ActivateNothingButNet()
    {
        netActive = true;
        points -= netPointCost;
    }
    public void ActivateSlowSat()
    {
        slowSatActive = true;
        points -= slowSatPointCost;
    }
    public void ActivateBreakThrough()
    {
        breakThroughActive = true;
        points -= breakThroughPointCost;
    }
}