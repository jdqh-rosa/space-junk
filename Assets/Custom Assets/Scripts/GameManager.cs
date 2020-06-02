using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//**// GAME MANAGER CODE //**//
//**// NFI //**//

//[ExecuteInEditMode]
public sealed class GameManager : MonoBehaviour
{
    static public Phase currentPhase = Phase.Past;

    [Header("Prefabs")]
    public GameObject[] earthPrefabs;
    public GameObject[] satellitePrefabs;
    public GameObject[] earthBasePrefabs;
    public GameObject[] rocketPrefabs;
    public GameObject[] spaceDebrisPrefabs;

    [Header("Trash Handler")]
    public TrashHandler trashHandler;

    [Header("Variables", order = 0)]

    [Header("Earth", order = 1)]
    public Vector3 earthLocation;
    public float worldRadius;
    public float earthRotation;

    [Header("Base")]
    //public int baseAmount;
    public Vector3 baseLocation;

    [Header("Satellite")]
    public float satelliteRadius;
    public float satelliteSpeed;

    [Header("Laser")]
    public KeyCode laserKey;
    public float laserFireRate;
    public float laserActiveLength;


    [Header("Rocket")]
    public float rocketLaunchHeight;
    public float rocketLaunchHeightRand;
    public float rocketLaunchSpeed;
    public float rocketFlightSpeed;
    public float rocketDestructTime;

    [Header("Trash")]
    public float trashRadius;
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

    [Header("Events", order = 0 )]

    [Header("Black Hole", order =1)]
    public float blackHoleChancePerMinute;
    public float blackHoleRadius;
    public float blackHoleDuration;

    [Header("GamePlay Blabla", order =0)]
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
        if(INSTANCE != null && INSTANCE != this)
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
        StreakHoldTimer();
        SlowSatTimer();
    }

    float holdStreakTimer;
    private void StreakHoldTimer()
    {
        holdStreakTimer+= Time.deltaTime;

        if(holdStreakDuration >= holdStreakTimer)
        {
            holdStreak= true;
        }
        else
        {
            holdStreak = false;
        }
    }

    float slowSatTimer;
    private void SlowSatTimer()
    {
        slowSatTimer+=Time.deltaTime;

        if(slowSatDuration >= slowSatTimer)
        {
            satelliteObject.GetComponent<Orbit>().orbitSpeed = slowSatSpeed;
        }
        else
        {
            satelliteObject.GetComponent<Orbit>().orbitSpeed = satelliteSpeed;
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

        if (GUILayout.Button("Create World"))
        {
            gm.CreateEarth();
        }
        if (GUILayout.Button("Create Base"))
        {
            gm.CreateBase();
        }
        if (GUILayout.Button("Create Satellite"))
        {
            gm.CreateSatellite();
        }

    }
}