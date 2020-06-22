using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreLoader : MonoBehaviour
{
    public GameObject FirstRowPrefab;
    public GameObject SecondRowPrefab;
    public GameObject RowPrefab;
    public GameObject FirstContainer;

    private float rowheight;
    private float currentY = 0;
    void Start()
    {
        Daily();
    }

    private void createTable(string query)
    {
        //Clear the current grid
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in FirstContainer.transform)
        {
            Destroy(child.gameObject);
        }

        currentY = -5;

        //Set rowheight to calculate height of other objects
        rowheight = RowPrefab.GetComponent<RectTransform>().sizeDelta.y;

        //Get all of the highscores
        dbConnection db = new dbConnection();
        List<highscore> scores = db.Select(query);

        //Loop trough all scores
        int pos = 1;
        foreach (highscore score in scores)
        {
            createRow(score, pos);
            pos++;
        }

        //Set the size of the content
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, rowheight * (scores.Count-1)+50);
    }

    /// <summary>
    /// This function creates a row, based on the given highscore and position
    /// </summary>
    /// <param name="pScore">highscore object with values</param>
    /// <param name="position">position value</param>
    private void createRow(highscore pScore, int position)
    {
        GameObject row;
        //Decide what prefab to use
        if(position == 1)
        {
            row = Instantiate(FirstRowPrefab, FirstContainer.transform, false);
            row.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        } else if(position == 2)
        {
            row = Instantiate(SecondRowPrefab, transform, false);
            row.GetComponent<RectTransform>().localPosition = new Vector3(0, currentY, 0);
            currentY -= rowheight;
        } else
        {
            row = Instantiate(RowPrefab, transform, false);
            row.GetComponent<RectTransform>().localPosition = new Vector3(0, currentY, 0);
            currentY -= rowheight;
        }

        //Set property values
        row.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("#"+position);
        row.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(pScore.Name);
        row.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(pScore.Score.ToString());
        row.transform.GetChild(3).GetComponent<TextMeshProUGUI>().SetText(pScore.Date.ToString("dd/MM/yyyy"));
    }

    public void Daily()
    {
        createTable("SELECT * FROM highscores WHERE DATE(Date) = DATE('now') ORDER BY Score DESC LIMIT 50");
    }

    public void Weekly()
    {
        createTable("SELECT * FROM highscores WHERE DATE(Date) >= DATE('now', 'weekday 0', '-7 days') ORDER BY Score DESC LIMIT 50");
    }

    public void Monthly()
    {
        createTable("SELECT * FROM highscores WHERE strftime('%m', Date) = strftime('%m', DATE('now')) ORDER BY Score DESC LIMIT 50");
    }

    public void Alltime()
    {
        createTable("SELECT * FROM highscores ORDER BY Score DESC LIMIT 50");
    }
}
