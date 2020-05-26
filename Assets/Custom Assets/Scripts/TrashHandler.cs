using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashHandler : MonoBehaviour
{
    public static int perc = 0;
    static List<bool> occupadoList = new List<bool>();
    static List<GameObject> trashList = new List<GameObject>();
    static Transform trashHandlerTF;
    void Start()
    {
        trashHandlerTF = transform;
    }


    void Update()
    {

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
}
