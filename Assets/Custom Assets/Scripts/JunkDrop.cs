using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkDrop : MonoBehaviour
{
    public GameObject trashPrefab;
    public int dropAmount;
    public int dropRand;
    public float dropGap;
    public Vector3 pivotPoint;
    private float radius;
    private GameObject[] cluster;
    Vector3 dropLocation;

    public float trashSpeed;
    public float trashSpeedRand;
    private float randomSpeed;

    public void Start()
    {
        dropAmount = Random.Range(dropAmount - dropRand, dropAmount + dropRand + 1);
        GameManager.Instance.RubbleDropped(dropAmount);
        cluster = new GameObject[dropAmount];
        radius = (transform.position - pivotPoint).magnitude;
        dropLocation = Helper.CalcDegToPos(Helper.CalcPosToDeg(pivotPoint, transform.position), radius - 1);

        for (int i = 0; i < dropAmount; ++i)
        {
            cluster[i] = Instantiate(trashPrefab, dropLocation, transform.rotation);
        }
        TrashHandler.AddToList(cluster[0]);

        randomSpeed = Random.Range(trashSpeed - trashSpeedRand, trashSpeed + trashSpeedRand);

        cluster[0].GetComponent<Orbit>().orbitSpeed = randomSpeed;
    }

    void Update()
    {
        for (int i = 1; i < cluster.Length; ++i)
        {
            if (cluster == null || cluster[i] == null){ continue; }

            if (cluster[i - 1] == null)
            {
                Destroy(cluster[i]);
            }

            if ((cluster[i].transform.position - cluster[i - 1].transform.position).magnitude >= 1)
            {
                TrashHandler.AddToList(cluster[i]);
                if (TrashHandler.speedChange)
                {
                    cluster[i].GetComponent<Orbit>().orbitSpeed = randomSpeed;
                }
                if (i == cluster.Length - 1)
                {
                    Destroy(this.gameObject);
                }
            }

        }
    }
}
