using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenKeyboard : MonoBehaviour
{
    public TMP_InputField Input;
    public TextMeshProUGUI CountText;
    public SceneManager sceneManager;
    public int MaxLength;

    private void Start()
    {
        CountText.SetText("0 / " + MaxLength);
    }

    public void KeyPress(string key)
    {
        if (Input != null)
        {
            switch(key)
            {
                case "backspace":
                    if(Input.text != "")
                    {
                        Input.text = Input.text.Remove(Input.text.Length - 1);
                    }
                    break;
                case "enter":
                    showHighscores();
                    break;
                default:
                    if(Input.text.Length < MaxLength)
                    {
                        Input.text += key;
                    }
                    break;
            }

            if(CountText != null)
            {
                CountText.SetText(Input.text.Length + " / " + MaxLength);
            }
        }
    }

    private void showHighscores()
    {
        //slide out the keyboard
        slideOut();

        //Add the highscore to the database
        dbConnection db = new dbConnection();
        highscore score = new highscore(0, PlayerPrefs.GetInt("score"), DateTime.Now, Input.text);
        db.InsertHighscore(score);
        db.Close();

        //Load the highscore screen
        sceneManager.LoadLevel(3);
    }

    private void slideOut()
    {
        GetComponent<Animator>().SetBool("Close", true);
    }
}
