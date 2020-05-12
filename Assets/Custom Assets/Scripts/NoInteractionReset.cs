using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoInteractionReset : MonoBehaviour
{
    public float ResetTimer = 30f;
    public SceneManager SceneManager;
    private float timeleft;

    private void Start()
    {
        //set initial time
        timeleft = ResetTimer;
    }

    void Update()
    {
        //Check if any input is received during this frame
        if(Input.anyKey)
        {
            //Reset the timer
            timeleft = ResetTimer;
        } else
        {
            //Subtract time from timer
            timeleft -= Time.deltaTime;

            //If no time is left, reset the game
            if (timeleft <= 0f)
            {
                Debug.Log("RESET GAME");
                if (SceneManager != null)
                {
                    SceneManager.LoadLevel(0);
                }
            }
        }
    }
}
