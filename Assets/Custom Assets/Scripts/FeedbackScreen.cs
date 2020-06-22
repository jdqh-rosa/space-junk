using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackScreen : MonoBehaviour
{
    public Slider funSlider;
    public Slider educationSlider;
    public SceneManager sceneManager;
    public void Submit()
    {
        int funscore = (int)funSlider.value;
        int educationscore = (int)educationSlider.value;

        dbConnection db = new dbConnection();
        int[] scores = new int[] { funscore, educationscore };
        db.InsertFeedback(scores);
    }
}
