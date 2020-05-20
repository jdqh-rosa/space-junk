using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkDrop : MonoBehaviour
{
    public GameObject trashPrefab;
    public int dropAmount;
    public float dropRange;
    public float spreadSpeed;
    public Vector3 pivotPoint;
    private float radius;
    private GameObject[] cluster;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cluster = new GameObject[dropAmount];
            radius = (transform.position-pivotPoint).magnitude;
            for (int i = 0; i < dropAmount; ++i)
            {
                Vector2 blah = Helper.CalculateDegPos((360 / dropAmount) * i, radius);
                cluster[i] = Instantiate(trashPrefab,
                new Vector3(blah.x, blah.y, 0),
                Quaternion.identity) as GameObject;
            }
        }
    }
}
