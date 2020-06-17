using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//**// GAME MANAGER INSPECTOR CODE //**//
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //serializedObject.Update();
        base.OnInspectorGUI();
        GameManager gm = (GameManager)target;


        if (GUILayout.Button("Create BlackHole"))
        {
            Instantiate(gm.blackHole);
        }
        if (GUILayout.Button("Spawn Alien"))
        {
            Instantiate(gm.aliens[0]);
        }
    }
}
