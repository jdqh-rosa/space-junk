using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//**// GAME MANAGER CODE //**//
//**// NFI //**//

//[ExecuteInEditMode]
public sealed class GameManager : MonoBehaviour
{
    private static readonly GameManager INSTANCE = new GameManager();

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


    GameObject earthObject;
    GameObject satelliteObject;
    GameObject baseObject;
    GameObject[] trashObjects;

    private GameManager()
    {

    }

    public static GameManager Instance
    {
        get
        {
            return INSTANCE;
        }
    }

    public void Start()
    {
        CreateEarth();
        CreateBase();
        CreateSatellite();
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

        if(GUILayout.Button("Create World")){
            gm.CreateEarth();
        }
        if(GUILayout.Button("Create Base"))
        {
            gm.CreateBase();
        }
        if(GUILayout.Button("Create Satellite"))
        {
            gm.CreateSatellite();
        }

    }
}