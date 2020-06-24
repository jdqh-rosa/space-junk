using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Translator : MonoBehaviour
{
    public string Dutch;
    public string English;
    public string German;

    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("Language", "NL");
        text = GetComponent<TextMeshProUGUI>();
        UpdateLanguage();
    }

    public void UpdateLanguage()
    {
        switch (PlayerPrefs.GetString("Language"))
        {
            case "NL":
                text.SetText(Dutch);
                break;
            case "DE":
                text.SetText(German);
                break;
            case "EN":
                text.SetText(English);
                break;
        }

        if (PlayerPrefs.GetString("Language") == null)
        {
            text.SetText(Dutch);
        }
    }
}
