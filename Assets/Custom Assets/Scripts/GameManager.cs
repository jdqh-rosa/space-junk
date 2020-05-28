using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//**// GAME MANAGER CODE //**//
//**// NFI //**//

//[ExecuteInEditMode]
public class GameManager : MonoBehaviour
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
    public int baseAmount;
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

    public bool init = false;

    GameObject earthObject;
    GameObject satelliteObject;
    GameObject baseObject;
    GameObject[] trashObjects;

    public void Start()
    {
        CreateEarth();
        CreateBase();
        CreateSatellite();
    }

    void CreateEarth()
    {
        earthObject = Instantiate(earthPrefabs[(int)currentPhase], earthLocation, Quaternion.identity);
    }

    void CreateBase()
    {
        baseLocation = Helper.CalcDegToPos(50, worldRadius);
        baseObject = Instantiate(earthBasePrefabs[(int)currentPhase], baseLocation, Quaternion.Euler(0,0,-50));
    }

    void CreateSatellite()
    {
        satelliteObject = Instantiate(satellitePrefabs[(int)currentPhase]);
        satelliteObject.GetComponent<Satellite>().target = earthObject;
        satelliteObject.GetComponent<Satellite>().lr = satelliteObject.GetComponent<LineRenderer>();
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
