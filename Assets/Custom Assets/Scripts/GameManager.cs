using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

//**// GAME MANAGER CODE //**//
//**// NFI //**//

//[ExecuteInEditMode]
public sealed class GameManager : MonoBehaviour
{
    static public float gameTime;
    static public float gameDeltaTime;
    static public float gameTimeScale = 1;
    public SceneManager sceneManager;

    [Header("UI")]
    public TextMeshProUGUI pointsText;
    public CustomSlider rubbleMeter;
    public CustomSlider actSlider;
    public TextMeshProUGUI multiplierText;
    public CustomSlider HoldStreakBtn;
    public CustomSlider SlowSatBtn;
    public CustomSlider BreakTroughBtn;
    public CustomSlider NetBtn;

    [Header("Prefabs")]
    public GameObject[] earthPrefabs;
    public GameObject[] satellitePrefabs;
    public GameObject[] earthBasePrefabs;
    public GameObject[] act1RocketPrefabs;
    public GameObject[] act2RocketPrefabs;
    public GameObject[] act3RocketPrefabs;
    public GameObject[] spaceDebrisPrefabs;
    public GameObject blackHole;
    public GameObject[] aliens;
    public GameObject netObject;
    public GameObject trashHub;

    [Header("Effects")]
    public GameObject beamEffect;
    public GameObject explosionEffect;
    public GameObject laserEffect;
    public GameObject shieldEffect;
    public GameObject rippleEffect;
    public GameObject breakThroughEffect;
    public GameObject[][] rockets = new GameObject[3][];

    [Header("Trash Handler")]
    public TrashHandler trashHandler;

    [Space(10, order = 0)]

    [Header("Variables", order = 1)]

    [HideInInspector] public int score = 0;
    [HideInInspector] public int points = 0;
    [HideInInspector] public int planetsexplored = 0;
    [HideInInspector] public int rocketslaunched = 0;
    [HideInInspector] public int rubbleremoved = 0;

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
    public AudioClip hitSound;
    public AudioClip missSound;
    public AudioClip shootSound;
    public bool targetAcquired;

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
    public int failedDropAmount;
    public int trashDropRand;
    public float trashGap;

    [HideInInspector]
    public int currentRubble = 0;

    [Header("Skills", order = 0)]

    [Header("Skill: Maintain Streak", order = 1)]
    public int holdStreakPointCost;
    public float holdStreakDuration;
    public bool holdStreak;
    public float holdStreakCooldown;
    [HideInInspector]
    public float holdStreakCurrentCooldown;

    [Header("Skill: Slow Satellite")]
    public int slowSatPointCost;
    public float slowSatDuration;
    public float slowSatSpeed;
    public float slowSatPercentage;
    public bool slowSatActive = false;
    public float slowSatCooldown;
    [HideInInspector]
    public float slowSatCurrentCooldown;

    [Header("Skill: Net")]
    public int netPointCost;
    public float netSpeed;
    public float netSize;
    public float netDuration;
    public bool netActive;
    public float netCooldown;
    [HideInInspector]
    public float netCurrentCooldown;

    [Header("Skill: Break Through")]
    public int breakThroughPointCost;
    public static bool breakThroughActive;
    public float breakTroughCooldown;
    [HideInInspector]
    public float breakTroughCurrentCooldown;

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
    [HideInInspector]
    public GameObject satelliteObject;
    public GameObject baseObject;
    GameObject[] trashObjects;

    private float percentage;

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
        rockets[0] = act1RocketPrefabs;
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
        rocketslaunched++;

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
                actSlider.value = percentage;
            }
        }
        else
        {
            actSlider.value = 1f;
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
        actSlider.value = 0f;
        actSlider.text = "ACT " + act;

        if (act == 3)
        {
            earthObject.GetComponent<Orbit>().orbitSpeed = -5;
        }
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
    /// 
   

    /// <summary>
    /// Transition to the end screen and show the results
    /// </summary>
    public void GameOver()
    {
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("rocketslaunched", rocketslaunched);
        PlayerPrefs.SetInt("planetsexplored", planetsexplored);
        PlayerPrefs.SetInt("rubbleremoved", rubbleremoved);
        sceneManager.LoadLevel(5);
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
        gameDeltaTime = Time.deltaTime * gameTimeScale;
        gameTime += gameDeltaTime;


        // Game over check
        percentage = (float)TrashHandler.ListCount() / (float)maxRubble;
        rubbleMeter.text = percentage.ToString("0%");
        rubbleMeter.value = percentage;
        if (percentage >= 1f)
        {
            GameOver();
        }
        StreakHoldTimer();
        SlowSatTimer();
        SpawnBlackHole();
        NothingButNet();
        manageCooldowns();
    }

    private void manageCooldowns()
    {
        if (holdStreakCurrentCooldown > 0f)
        {
            holdStreakCurrentCooldown -= gameDeltaTime;
            HoldStreakBtn.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            HoldStreakBtn.gameObject.GetComponent<Button>().interactable = true;
        }

        if (slowSatCurrentCooldown > 0f)
        {
            slowSatCurrentCooldown -= gameDeltaTime;
            SlowSatBtn.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            SlowSatBtn.gameObject.GetComponent<Button>().interactable = true;
        }

        if (netCurrentCooldown > 0f)
        {
            netCurrentCooldown -= gameDeltaTime;
            NetBtn.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            NetBtn.gameObject.GetComponent<Button>().interactable = true;
        }

        if (breakTroughCurrentCooldown > 0f)
        {
            breakTroughCurrentCooldown -= gameDeltaTime;
            BreakTroughBtn.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            BreakTroughBtn.gameObject.GetComponent<Button>().interactable = true;
        }

        //Calculate percentages
        HoldStreakBtn.value = (holdStreakCooldown - holdStreakCurrentCooldown) / holdStreakCooldown;
        SlowSatBtn.value = (slowSatCooldown - slowSatCurrentCooldown) / slowSatCooldown;
        NetBtn.value = (netCooldown - netCurrentCooldown) / netCooldown;
        BreakTroughBtn.value = (breakTroughCooldown - breakTroughCurrentCooldown) / breakTroughCooldown;
        //Debug.Log(holdStreakCurrentCooldown);
    }

    /// <summary>
    /// turns the skill off once a certain amount of time has been reached
    /// </summary>
    float holdStreakTimer;
    private void StreakHoldTimer()
    {
        if (holdStreak)
        {
            if (holdStreakTimer == 0)
            {
                Instantiate(shieldEffect, satelliteObject.transform);
            }
            holdStreakCurrentCooldown = holdStreakCooldown;
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
            slowSatCurrentCooldown = slowSatCooldown;
            slowSatTimer += GameManager.gameDeltaTime;
            if (slowSatDuration >= slowSatTimer)
            {
                satelliteObject.GetComponent<Orbit>().orbitSpeed = slowSatSpeed;
                GameManager.gameTimeScale = 0.5f;
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
            netCurrentCooldown = netCooldown;
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

    /// <summary>
    /// has a chance of spawning an alien every minute
    /// </summary>
    float truckTimer = 0;
    void SpawnTruck()
    {
        truckTimer += GameManager.gameDeltaTime;
        if (truckTimer >= 60)
        {
            if (Random.Range(0f, 1f) <= garbageTruckChancePerMinute / 100f)
            {
                Instantiate(aliens[act - 1]);
            }
            truckTimer = 0;
        }
    }


    //**// Methods to switch skill bools//**//
    public void ActivateHoldStreak()
    {
        holdStreak = true;
    }
    public void ActivateNothingButNet()
    {
        netActive = true;
    }
    public void ActivateSlowSat()
    {
        slowSatActive = true;
    }
    public void ActivateBreakThrough()
    {
        breakThroughActive = true;
    }
}