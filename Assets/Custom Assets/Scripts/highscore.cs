﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Data class containing highscore info
/// </summary>
public class highscore
{
    public int ID;
    public int Score;
    public System.DateTime Date;
    public string Name;

    /// <summary>
    /// Data class that contains highscore data
    /// </summary>
    /// <param name="pID">Unique ID </param>
    /// <param name="pScore">Score of the entry</param>
    /// <param name="pDate">Date the entry was added to the database</param>
    /// <param name="pName">Name that was entered</param>
    public highscore(int pID, int pScore, System.DateTime pDate, string pName)
    {
        ID = pID;
        Score = pScore;
        Date = pDate;
        Name = pName;
    }
}
