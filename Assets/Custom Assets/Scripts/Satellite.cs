﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//**// Code for Satellite and Laser interaction //**//
public class Satellite : MonoBehaviour
{
    public float laserRate = 1;
    public float laserDuration = 0.1f;
    public GameObject target;
    public LineRenderer lr;
    public Orbit orbit;

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
            if (Input.GetMouseButtonUp(0))
            {
                if (!lr.enabled) { lr.enabled = true; }
                PewPew();
                laserCountDown = 1 / laserRate;
                laserTimer = laserDuration;
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

        laserCountDown -= Time.deltaTime;
        laserTimer -= Time.deltaTime;
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
            if (hit.collider)
            {
                if (hit.collider.gameObject == target)
                {
                    print("hit target");
                    gameObject.GetComponent<Renderer>().material.color = Color.red;
                    lr.SetPosition(1, target.gameObject.transform.position);
                }
                lr.SetPosition(1, hit.point);
                //print("hit");
            }
        }
    }
}
