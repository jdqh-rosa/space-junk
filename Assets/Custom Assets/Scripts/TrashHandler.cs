using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashHandler : MonoBehaviour
{
    public static int perc = 0;
    static List<GameObject> trashList = new List<GameObject>();
    static Transform trashHandlerTF;

    public float minGapLength = 1;
    public float trashSpeed;
    public float trashSpeedRand;

    static public bool speedChange = true;
    
    private List<float> degreeList = new List<float>();

    void Start()
    {
        trashHandlerTF = transform;
    }

    public static void AddToList(GameObject gameObj)
    {
        if (!trashList.Contains(gameObj))
        {
            trashList.Add(gameObj);
            gameObj.transform.parent = trashHandlerTF;
            ++perc;
        }
    }

    void TrashCollision()
    {
        for (int i = 0; i < trashList.Count; ++i)
        {
            if (trashList[i] == null) { trashList.RemoveAt(i); }
            for (int j = i + 1; j < trashList.Count; ++j)
            {
                if (trashList[j] == null)
                {
                    trashList.RemoveAt(j);
                    if(j>trashList.Count-1) continue;
                }

                if ((trashList[i].transform.position - trashList[j].transform.position).magnitude <= minGapLength)
                {
                    //trashList[j].SetActive(false);
                    Destroy(trashList[j]);
                    trashList.RemoveAt(j);
                }
            }
            degreeList.Add(Helper.CalcPosToDeg(Vector3.zero, trashList[i].transform.position));
        }
    }

    void Update()
    {
        TrashCollision();
    }
}
