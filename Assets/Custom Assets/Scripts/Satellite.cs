using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//**// Code for Satellite and Laser interaction //**//
public class Satellite : MonoBehaviour
{
    public float laserRate = 1;
    public float laserDuration = 0.1f;
    public KeyCode laserKey;
    public GameObject target;
    public LineRenderer lr;

    RaycastHit hit;
    public bool switchBase = true;

    AudioSource audioSource;

    private float laserCountDown = 0;
    private float laserTimer = 0;

    void Start()
    {
        laserKey = GameManager.Instance.laserKey;
        audioSource = GetComponent<AudioSource>();
        lr.enabled = false;
        lr.SetWidth(.2f, .2f);

        audioSource.clip = GameManager.Instance.shootSound;

    }
    void Update()
    {
        //CheckTarget();
        if (Input.GetKeyDown(laserKey))
        {
            audioSource.clip = GameManager.Instance.shootSound;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "click")
                {
                    if (GameManager.Instance.tutorialActive)
                    {
                        if (GameManager.Instance.shoot)
                        {
                            PewPew();
                        }
                    }
                    else if (laserCountDown <= 0)
                    {
                        PewPew();
                        laserCountDown = 1 / laserRate;
                        laserTimer = laserDuration;
                    }
                }
            }
        }

        laserCountDown -= GameManager.gameDeltaTime;
        laserTimer -= GameManager.gameDeltaTime;
    }

    //CHECK IF THE LASER HITS THE TARGET AND SET THE DISTANCE TO RAYCAST HIT
    void PewPew()
    {
        if (target == null)
        {
            if (lr.enabled) { lr.enabled = false; }
            return;
        }

        audioSource.Play();

        Vector3 bla = -transform.up * 6;



        lr.SetPosition(1, target.transform.position);
        if (Physics.Raycast(transform.position, Vector3.Normalize(target.gameObject.transform.position - transform.position), out hit))
        {
            if (hit.collider)
            {
                //lr.SetPosition(1, hit.point);
                if (hit.collider.gameObject.tag == "Base")
                {
                    print("hit target");
                    lr.SetPosition(1, target.gameObject.transform.position);

                    if (GameManager.breakThroughActive)
                    {
                        //GameManager.Instance.baseObject.GetComponent<SpawnRocket>().ImperviousLaunch();
                        Instantiate(GameManager.Instance.breakThroughEffect, transform.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 180));
                        GameManager.breakThroughActive=false;
                    }
                    else
                    {
                        Instantiate(GameManager.Instance.laserEffect, transform.position + bla, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 90), transform);
                        GameManager.Instance.baseObject.GetComponent<SpawnRocket>().Launch();
                    }
                    GameManager.Instance.BeamHit();
                }
                else
                {
                    audioSource.clip = GameManager.Instance.missSound;
                    audioSource.Play();
                    Instantiate(GameManager.Instance.laserEffect, transform.position + bla, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 90), transform);
                    lr.SetPosition(1, hit.collider.gameObject.transform.position);
                    GameManager.Instance.baseObject.GetComponent<SpawnRocket>().FaultyLaunch();
                    GameManager.Instance.BeamMissed();
                }
            }
        }
    }


    void LaunchRocket()
    {
        if (GameManager.breakThroughActive)
        {
            GameManager.Instance.baseObject.GetComponent<SpawnRocket>().ImperviousLaunch();
        }
        else if (GameManager.Instance.targetAcquired)
        {
            GameManager.Instance.baseObject.GetComponent<SpawnRocket>().Launch();
        }
        else
        {
            GameManager.Instance.baseObject.GetComponent<SpawnRocket>().FaultyLaunch();
        }
        laserCountDown = 1 / laserRate;
    }

}


