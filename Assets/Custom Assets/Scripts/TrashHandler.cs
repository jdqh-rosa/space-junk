using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashHandler : MonoBehaviour
{
    public static int perc = 0;
    static List<bool> occupadoList = new List<bool>();
    static List<GameObject> trashList = new List<GameObject>();
    static Transform trashHandlerTF;

    public float minGapLength=1;

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
        for(int i=0; i<trashList.Count; ++i)
        {
            for(int j=i+1; j<trashList.Count; ++j)
            {
                if((trashList[i].transform.position - trashList[j].transform.position).magnitude <= minGapLength)
                {
                    trashList[j].SetActive(false);
                    trashList.RemoveAt(j);
                }
            }
        }
    }

    void Update()
    {
        TrashCollision();
    }
}
