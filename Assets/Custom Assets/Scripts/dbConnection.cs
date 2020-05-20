using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Microsoft.Data.Sqlite;

/// <summary>
/// Database helper class to easily read, modify and delete records from the database
/// </summary>
public class dbConnection {

    private SqliteConnection connection;
    public dbConnection()
    {
        connection = new SqliteConnection("Filename="+Application.dataPath+"Custom Assets/database.db");
    }
}
