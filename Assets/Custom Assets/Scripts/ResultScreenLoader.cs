using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultScreenLoader : MonoBehaviour
{
    public TextMeshProUGUI Scoretext;
    // Start is called before the first frame update
    void Start()
    {
        Scoretext.SetText("" + PlayerPrefs.GetInt("score"));
    }
}
