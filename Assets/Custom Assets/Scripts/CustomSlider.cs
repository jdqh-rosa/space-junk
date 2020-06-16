using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CustomSlider : MonoBehaviour
{
    [Range(0f, 1f)]
    public float value = 0f;
    public string text = "ACT 1";
    public float animationSpeed = 1f;

    public Image fillArea;
    public TextMeshProUGUI textArea;
    public Button btn;

    // Update is called once per frame
    void Update()
    {
        if(fillArea != null)
        {
            fillArea.fillAmount = Mathf.SmoothStep(fillArea.fillAmount, value, animationSpeed);
        }

        if(textArea != null)
        {
            textArea.SetText(text);
        }

        if(btn != null)
        {
            switch(gameObject.name)
            {
                case "HoldStreak":
                    value = GameManager.Instance.holdStreakDuration;
                    break;
                case "SlowDown":
                    break;
                case "BreakTrough":
                    break;
                case "DeployNet":
                    break;
            }
        }
    }
}
