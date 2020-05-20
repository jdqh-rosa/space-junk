using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreLoader : MonoBehaviour
{
    public GameObject RowPrefab;

    private float rowheight;
    private float currentY = 0;
    void Start()
    {
        //Set rowheight to calculate height of other objects
        rowheight = RowPrefab.GetComponent<RectTransform>().sizeDelta.y;

        //Get all of the highscores
        dbConnection db = new dbConnection();
        List<highscore> scores = db.Select("SELECT * FROM highscores ORDER BY Score DESC");

        //Loop trough all scores
        int pos = 1;
        foreach(highscore score in scores)
        {
            createRow(score, pos);
            pos++;
        }

        //Set the size of the content
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, rowheight * scores.Count);
    }

    /// <summary>
    /// This function creates a row, based on the given highscore and position
    /// </summary>
    /// <param name="pScore">highscore object with values</param>
    /// <param name="position">position value</param>
    private void createRow(highscore pScore, int position)
    {
        //Set position values
        GameObject row = Instantiate(RowPrefab, transform, false);
        row.GetComponent<RectTransform>().localPosition = new Vector3(0,currentY,0);
        currentY -= rowheight;

        //Set property values
        row.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("#"+position);
        row.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(pScore.Name);
        row.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(pScore.Score.ToString());
        row.transform.GetChild(3).GetComponent<TextMeshProUGUI>().SetText(pScore.Date.ToShortDateString());
    }
}
