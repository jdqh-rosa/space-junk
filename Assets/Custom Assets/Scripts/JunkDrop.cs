using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkDrop : MonoBehaviour
{
    public GameObject trashPrefab;
    public int dropAmount;
    public int dropRand;
    //public float dropRange;
    public float dropGap;
    //public float spreadSpeed;
    public Vector3 pivotPoint;
    private float radius;
    private GameObject[] cluster;
    private float locatDeg;
    //Vector2 relRange;
    Vector3 dropLocation;

    public float trashSpeed;
    public float trashSpeedRand;
    private float randomSpeed;

    public void Start()
    {
        dropLocation = transform.position;
        dropAmount = Random.Range(dropAmount - dropRand, dropAmount + dropRand + 1);
        cluster = new GameObject[dropAmount];
        radius = (dropLocation - pivotPoint).magnitude;
        locatDeg = Helper.CalcPosToDeg(pivotPoint, new Vector2(dropLocation.x, dropLocation.y));
        //relRange = new Vector2(locatDeg - (dropAmount * dropGap) / 2, locatDeg + (dropGap * dropAmount) / 2);
        //relRange = new Vector2(locatDeg - dropRange / 2, locatDeg + dropRange / 2);
        for (int i = 0; i < dropAmount; ++i)
        {
            cluster[i] = Instantiate(trashPrefab, dropLocation * 0.9f, transform.rotation);
        }
        TrashHandler.AddToList(cluster[0]);

        randomSpeed = Random.Range(trashSpeed - trashSpeedRand, trashSpeed + trashSpeedRand);

        cluster[0].GetComponent<Orbit>().orbitSpeed = randomSpeed;
    }

    void Update()
    {
        for (int i = 1; i < dropAmount; ++i)
        {
            if (cluster == null || cluster[i] == null) return;

            if ((cluster[i].transform.position - cluster[i - 1].transform.position).magnitude >= 1)
            {
                TrashHandler.AddToList(cluster[i]);
                if (TrashHandler.speedChange)
                {
                    cluster[i].GetComponent<Orbit>().orbitSpeed = randomSpeed;
                }
            }
        }
        //gameObject.GetComponent<Rocket>().destroy = destroy;
    }
}
