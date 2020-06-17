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

    public RaycastHit rayCastHit;
    public bool switchBase = true;

    void Start()
    {
        lr.enabled = false;
        lr.SetWidth(.2f, .2f);
    }
    void Update()
    {
        ShootLaser();
    }

    float laserCountDown = 0;
    float laserTimer = 0;
    void ShootLaser()
    {

        lr.SetPosition(0, transform.position);

        //COOLDOWN FOR THE LASER
        if (laserCountDown <= 0)
        {
            if (GameManager.breakThroughActive)
            {
                if (Input.GetKeyDown(laserKey))
                {
                    if (!lr.enabled) { lr.enabled = true; }
                    BreakThroughShot();
                    GameManager.breakThroughActive = false;
                    GameManager.Instance.breakTroughCurrentCooldown = GameManager.Instance.breakTroughCooldown;
                }
            }

            if (Input.GetKeyDown(laserKey))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "click")
                    {
                        if (!lr.enabled) { lr.enabled = true; }
                        PewPew();
                        laserCountDown = 1 / laserRate;
                        laserTimer = laserDuration;
                    }
                }
            }
        }
        //HOW LONG THE LASER STAYS ON
        if (lr.enabled)
        {
            if (laserTimer <= 0)
            {
                lr.enabled = false;
                gameObject.GetComponent<Renderer>().material.color = Color.white;
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

        RaycastHit hit;

        lr.SetPosition(1, target.transform.position);
        if (Physics.Raycast(transform.position, Vector3.Normalize(target.gameObject.transform.position - transform.position), out hit))
        {
            rayCastHit = hit;
            if (hit.collider)
            {
                //lr.SetPosition(1, hit.point);
                if (hit.collider.gameObject.tag == "Base")
                {
                    print("hit target");
                    gameObject.GetComponent<Renderer>().material.color = Color.red;
                    if(!GameManager.breakThroughActive){
                    hit.collider.gameObject.GetComponent<SpawnRocket>().Launch();
                    }else { hit.collider.gameObject.GetComponent<SpawnRocket>().ImperviousLaunch(); }
                    Destroy(hit.collider.gameObject);
                    GameManager.Instance.CreateBase();
                    lr.SetPosition(1, target.gameObject.transform.position);

                    GameManager.Instance.BeamHit();
                }
                else
                {
                    lr.SetPosition(1, hit.collider.gameObject.transform.position);
                    GameManager.Instance.BeamMissed();
                }
            }
        }
    }

    void BreakThroughShot()
    {
        if (target == null)
        {
            if (lr.enabled) { lr.enabled = false; }
            return;
        }

        RaycastHit hit;

        lr.SetPosition(1, target.transform.position);
        if (Physics.Raycast(transform.position, Vector3.Normalize(target.gameObject.transform.position - transform.position), out hit))
        {
            rayCastHit = hit;
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
                if (hit.collider.gameObject.tag == "TrashJunk")
                {
                    print("BREEEEEEAAAAAAK THROOOOOOOOUUUUUUGGGGHHHH!!!!!!!!!!");
                    gameObject.GetComponent<Renderer>().material.color = Color.magenta;
                    Destroy(hit.collider.gameObject);
                }
                PewPew();
            }
        }
    }
}
