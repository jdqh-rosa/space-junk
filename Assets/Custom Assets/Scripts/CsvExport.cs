using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
public class CsvExport : MonoBehaviour
{
    public void Export()
    {
        List<string[]> rowData = new List<string[]>();

        //Add the date to the file
        string[] rowDataTemp = new string[1];
        rowDataTemp[0] = "Highscores overzicht";
        rowData.Add(rowDataTemp);

        // Create the row headers
        rowDataTemp = new string[4];
        rowDataTemp[0] = "POSITIE";
        rowDataTemp[1] = "NAAM";
        rowDataTemp[2] = "SCORE";
        rowDataTemp[3] = "DATUM";
        rowData.Add(rowDataTemp);

        // Create a database connection
        dbConnection db = new dbConnection();
        List<highscore> scores = db.Select("SELECT * FROM highscores ORDER BY Score DESC");

        // Loop trough highscores and add them to new rows
        int pos = 1;
        foreach (highscore score in scores)
        {
            rowDataTemp = new string[4];
            rowDataTemp[0] = "#"+pos;
            rowDataTemp[1] = score.Name;
            rowDataTemp[2] = ""+ score.Score;
            rowDataTemp[3] = "" + score.Date.ToString("dd/MM/yyyy");
            rowData.Add(rowDataTemp);
            pos++;
        }

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = "|";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = Application.dataPath + "/StreamingAssets/highscore_export.csv";

        StreamWriter outStream = File.CreateText(filePath);
        outStream.WriteLine("sep=|");
        outStream.WriteLine(sb);
        outStream.Close();
    }

    public void ExportFeedback()
    {
        List<string[]> rowData = new List<string[]>();

        //Add the date to the file
        string[] rowDataTemp = new string[1];
        rowDataTemp[0] = "Feedback overzicht";
        rowData.Add(rowDataTemp);

        // Create the row headers
        rowDataTemp = new string[4];
        rowDataTemp[0] = "PLEZIER";
        rowDataTemp[1] = "EDUCATIEF";
        rowData.Add(rowDataTemp);

        // Create a database connection
        dbConnection db = new dbConnection();
        List<int[]> feedbackList = db.SelectFeedback("SELECT * FROM feedback");

        // Loop trough highscores and add them to new rows
        int pos = 1;
        foreach (int[] feedback in feedbackList)
        {
            rowDataTemp = new string[2];
            rowDataTemp[0] = ""+feedback[0];
            rowDataTemp[1] = "" + feedback[1];
            rowData.Add(rowDataTemp);
            pos++;
        }

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = "|";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = Application.dataPath + "/StreamingAssets/feedback_export.csv";

        StreamWriter outStream = File.CreateText(filePath);
        outStream.WriteLine("sep=|");
        outStream.WriteLine(sb);
        outStream.Close();
    }
}