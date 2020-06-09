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
        holdSteak.interactable=false;
        slowSatellite.interactable=false;
        netThrow.interactable=false;
        breakShot.interactable=false;
    }

    void Update()
    {
        if (GameManager.score > streakScore) { holdSteak.interactable = true; }
        else { holdSteak.interactable = false; }

        if (GameManager.score > slowScore) { slowSatellite.interactable = true;  }
        else { slowSatellite.interactable = false; }

        if (GameManager.score > netScore) { netThrow.interactable = true; }
        else { netThrow.interactable = false; }

        if (GameManager.score > breakScore) { breakShot.interactable = true; }
        else { breakShot.interactable = false; }
    }
}
