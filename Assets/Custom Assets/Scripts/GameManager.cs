﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//**// GAME MANAGER CODE //**//
//**// NFI //**//

//[ExecuteInEditMode]
public sealed class GameManager : MonoBehaviour
{
    static public float gameTime;
    static public float gameDeltaTime;
    static public Phase currentPhase = Phase.Past;

    [Header("Prefabs")]
    public GameObject[] earthPrefabs;
    public GameObject[] satellitePrefabs;
    public GameObject[] earthBasePrefabs;
    public GameObject[] rocketPrefabs;
    public GameObject[] spaceDebrisPrefabs;
    public GameObject blackHole;

    [Header("Trash Handler")]
    public TrashHandler trashHandler;

    [Header("Variables", order = 0)]

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

    [Header("Skills", order = 0)]

    [Header("Skill: Maintain Streak", order = 1)]
    public float holdStreakDuration;
    public bool holdStreak;

    [Header("Skill: Slow Satellite")]
    public float slowSatDuration;
    public float slowSatSpeed;
    public float slowSatPercentage;
    public bool slowSatActive = false;

    [Header("Events", order = 0)]

    [Header("Black Hole", order = 1)]
    public float blackHoleChancePerMinute;
    public float blackHoleRadius;
    public float blackHoleDuration;

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
        Instance.CreateEarth();
        Instance.CreateBase();
        Instance.CreateSatellite();
        Instance.TrashHandlerVars();
    }

    public void CreateEarth()
    {
        earthObject = Instantiate(earthPrefabs[(int)currentPhase], earthLocation, Quaternion.identity);
    }

    public void CreateBase()
    {
        int rand = Random.Range(0, 360);
        baseObject = Instantiate(earthBasePrefabs[(int)currentPhase], Helper.CalcDegToPos(rand, worldRadius), Quaternion.Euler(0, 0, rand - 90));
        baseObject.transform.parent = earthObject.transform;
    }

    public void CreateSatellite()
    {
        satelliteObject = Instantiate(satellitePrefabs[(int)currentPhase]);
        satelliteObject.GetComponent<Orbit>().radius = satelliteRadius;
        satelliteObject.GetComponent<Orbit>().orbitSpeed = satelliteSpeed;
        satelliteObject.GetComponent<Satellite>().target = earthObject;
        satelliteObject.GetComponent<Satellite>().lr = satelliteObject.GetComponent<LineRenderer>();
        satelliteObject.GetComponent<Satellite>().laserRate = laserFireRate;
        satelliteObject.GetComponent<Satellite>().laserKey = laserKey;
        satelliteObject.GetComponent<Satellite>().laserDuration = laserActiveLength;
        GetComponent<HitDetection>().satellite = satelliteObject;
    }



    void Update()
    {
        gameTime += Time.deltaTime;
        gameDeltaTime = Time.deltaTime;

        StreakHoldTimer();
        SlowSatTimer();
    }

    float holdStreakTimer;
    private void StreakHoldTimer()
    {
        if (holdStreak)
        {
            holdStreakTimer += GameManager.gameDeltaTime;

            if (holdStreakDuration <= holdStreakTimer)
            {
                holdStreak= false;
                holdStreakTimer=0;
            }
        }
    }

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

    void TrashHandlerVars()
    {

        trashHandler.trashSpeedRand = trashDropRand;
        trashHandler.minGapLength = trashGap;
    }

    float blackHoleTimer = 0;
    void SpawnBlackHole()
    {
        blackHoleTimer += GameManager.gameDeltaTime;
        if (blackHoleTimer >= 60)
        {
            if (Random.Range(0f, 1f) <= blackHoleChancePerMinute/100f)
            {
                Instantiate(blackHole);
            }
            blackHoleTimer = 0;
        }
    }

}

public enum Phase
{
    Past,
    Present,
    Future
}


//**// GAME MANAGER INSPECTOR CODE //**//
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //serializedObject.Update();
        base.OnInspectorGUI();
        GameManager gm = (GameManager)target;


        if (GUILayout.Button("Create BlackHole"))
        {
            Instantiate(gm.blackHole);
        }

    }
}