using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System;

/// <summary>
/// Database helper class to easily read, modify and delete records from the database
/// </summary>
public class dbConnection
{
    private string connectionString, query;
    private IDbConnection connection;
    private IDbCommand command;
    private IDataReader reader;
    public dbConnection()
    {
        connectionString = "URI=file:" + Application.dataPath + "/Custom Assets/database.db";

        Debug.Log("Stablishing connection to: " + connectionString);
        connection = new SqliteConnection(connectionString);
        connection.Open();
    }

    /// <summary>
    /// Select function to select highscores
    /// </summary>
    /// <param name="Query">Query to be executed by sqlite</param>
    /// <returns>A list of highscores returned by the query</returns>
    public List<highscore> Select(string pQuery)
    {
        //Create the list
        List<highscore> highScores = new List<highscore>();

        //Create the sql command
        command = connection.CreateCommand();
        //Set command text
        command.CommandText = pQuery;
        //Execute the sql command
        IDataReader reader = command.ExecuteReader();

        //This while loops over all found entries (1 loop for each found entry)
        while (reader.Read())
        {
            //Get the entry values from the reader
            int id = reader.GetInt32(0);
            int score = reader.GetInt32(1);
            System.DateTime date = reader.GetDateTime(2);
            string name = reader.GetString(3);

            //Add the entry to the output list
            highScores.Add(new highscore(id, score, date, name));
        }

        //Close the reader
        reader.Close();
        //Dispose the command
        command.Dispose();

        //Return the result(s)
        return highScores;
    }

    /// <summary>
    /// Select function to select highscores
    /// </summary>
    /// <param name="Query">Query to be executed by sqlite</param>
    /// <returns>A list of highscores returned by the query</returns>
    public List<int[]> SelectFeedback(string pQuery)
    {
        //Create the list
        List<int[]> feedback = new List<int[]>();

        //Create the sql command
        command = connection.CreateCommand();
        //Set command text
        command.CommandText = pQuery;
        //Execute the sql command
        IDataReader reader = command.ExecuteReader();

        //This while loops over all found entries (1 loop for each found entry)
        while (reader.Read())
        {
            //Get the entry values from the reader
            int fun = reader.GetInt32(0);
            int education = reader.GetInt32(1);

            //Add the entry to the output list
            feedback.Add(new int[] {fun,education });
        }

        //Close the reader
        reader.Close();
        //Dispose the command
        command.Dispose();

        //Return the result(s)
        return feedback;
    }

    public float GetAverage(string field)
    {
        //Create the list
        float avg = 0;

        //Create the sql command
        command = connection.CreateCommand();
        //Set command text
        command.CommandText = "SELECT avg("+field+") FROM highscores;";
        //Execute the sql command
        IDataReader reader = command.ExecuteReader();

        //This while loops over all found entries (1 loop for each found entry)
        while (reader.Read())
        {
            //Get the entry values from the reader
            avg = reader.GetFloat(0);
        }

        //Close the reader
        reader.Close();
        //Dispose the command
        command.Dispose();

        //Return the result(s)
        return avg;
    }

    public void InsertHighscore(highscore pScore)
    {
        command = connection.CreateCommand();
        command.CommandText = String.Format("INSERT INTO highscores (Score, Name) VALUES ({0},'{1}')", pScore.Score, pScore.Name);
        command.ExecuteNonQuery();
        command.Dispose();
    }

    public void InsertFeedback(int[] scores)
    {
        command = connection.CreateCommand();
        command.CommandText = String.Format("INSERT INTO feedback (fun, education) VALUES ({0},{1})", scores[0], scores[1]);
        command.ExecuteNonQuery();
        command.Dispose();
    }

    public void Close()
    {
        connection.Close();
    }
}
