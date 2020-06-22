using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultScreenLoader : MonoBehaviour
{
    public TextMeshProUGUI Scoretext;
    public TextMeshProUGUI planetsexploredYou;
    public TextMeshProUGUI planetsexploredAverage;
    public TextMeshProUGUI rocketslaunchedYou;
    public TextMeshProUGUI rocketslaunchedAverage;
    public TextMeshProUGUI rubbleremovedYou;
    public TextMeshProUGUI rubbleremovedAverage;

    // Start is called before the first frame update
    void Start()
    {
        Scoretext.SetText("" + PlayerPrefs.GetInt("score"));
        if(planetsexploredYou != null)
        {
            planetsexploredYou.SetText("" + PlayerPrefs.GetInt("planetsexplored"));
            rocketslaunchedYou.SetText("" + PlayerPrefs.GetInt("rocketslaunched"));
            rubbleremovedYou.SetText("" + PlayerPrefs.GetInt("rubbleremoved"));

            //Calculate average scores
            dbConnection db = new dbConnection();
            planetsexploredAverage.SetText("" + db.GetAverage("PlanetsExplored"));
            rocketslaunchedAverage.SetText("" + db.GetAverage("RocketsLaunched"));
            rubbleremovedAverage.SetText("" + db.GetAverage("RubbleCleared"));
        }
    }
}
