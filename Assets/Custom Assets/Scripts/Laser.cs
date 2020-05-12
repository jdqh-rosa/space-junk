using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject target;
     LineRenderer lr;
     
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetWidth(.2f,.2f);
    }

    void Update()
    {
        print(transform.position);
        lr.SetPosition(0, transform.position);
            RaycastHit hit;
            if(target!=null) lr.SetPosition(1,target.transform.position);
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit. collider)
                {
                    lr.SetPosition(1, hit.point);
                    //print("hit");
                    //gameObject.SetActive(false);
                }
            }
        else {
        lr.SetPosition(1, transform.position + (transform.forward * 5000));
        }
    }

    
}
