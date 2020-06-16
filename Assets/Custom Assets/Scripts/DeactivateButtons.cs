using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateButtons : MonoBehaviour
{
    public UnityEngine.UI.Selectable holdSteak;
    public int streakScore;
    public UnityEngine.UI.Selectable slowSatellite;
    public int slowScore;
    public UnityEngine.UI.Selectable netThrow;
    public int netScore;
    public UnityEngine.UI.Selectable breakShot;
    public int breakScore;

    private void Start()
    {
        //holdSteak.interactable=false;
        //slowSatellite.interactable=false;
        //netThrow.interactable=false;
        //breakShot.interactable=false;
    }

    void Update()
    {
        //int points = GameManager.Instance.points;
        //if (points > GameManager.Instance.holdStreakPointCost) { holdSteak.interactable = true; }
        //else { holdSteak.interactable = false; }

        //if (points > GameManager.Instance.slowSatPointCost) { slowSatellite.interactable = true;  }
        //else { slowSatellite.interactable = false; }

        //if (points > GameManager.Instance.netPointCost) { netThrow.interactable = true; }
        //else { netThrow.interactable = false; }

        //if (points > GameManager.Instance.breakThroughPointCost) { breakShot.interactable = true; }
        //else { breakShot.interactable = false; }
    }
}
