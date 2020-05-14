using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//**// CONTROL ORBITTING TRASH //**//
public class TrashRing : MonoBehaviour
{
    public Vector3 pivotPoint;
    public float radius;
    public int amount = 1;
    public GameObject debrisPrefab;
    public TrashCloud[] clouds;
    public int debrisLocation;

    public void CreateDebris()
    {
        if (clouds != null) DestroyDebris();
        clouds = new TrashCloud[amount];
        for (int i = 0; i < amount; ++i)
        {
            clouds[i].radius = radius;
            clouds[i].locatDeg = (360 / amount) * i;
            Vector2 blah = Helper.CalculateDegPos((360 / amount) * i, radius);
            clouds[i].debris = Instantiate(debrisPrefab,
            new Vector3(blah.x, blah.y, 0),
            Quaternion.identity) as GameObject;

            //clouds[i].debris.transform.parent = transform;
        }
    }

    public void InstantiateCloud()
    {
        for (int u = 0; u < clouds.Length; ++u)
        {
            clouds[u].debrisCloud = new GameObject[clouds[u].cloudSize];
            for (int i = 0; i < clouds[u].cloudSize; ++i)
            {
                clouds[u].debrisCloud[i] = Instantiate(clouds[u].debris) as GameObject;
            }
        }
    }

    void DestroyDebris()
    {
        for (int i = 0; i < clouds.Length; ++i)
        {
            if (clouds[i].debris != null) DestroyImmediate(clouds[i].debris, true);
        }
        clouds = null;
    }

    public void DestroyLostDebris()
    {
        GameObject[] lostMom = GameObject.FindGameObjectsWithTag("TrashHub");
        GameObject[] lostJunk = GameObject.FindGameObjectsWithTag("TrashJunk");

        foreach (GameObject g in lostJunk)
        {
            DestroyImmediate(g);
        }
        foreach (GameObject g in lostMom)
        {
            DestroyImmediate(g);
        }
    }

}


[CustomEditor(typeof(TrashRing))]
public class TrashRingEditor : Editor
{
    bool debris;
    bool cloud;
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        TrashRing tr = (TrashRing)target;

        //DrawDefaultInspector();

        tr.pivotPoint = EditorGUILayout.Vector3Field("Pivot Point", tr.pivotPoint);
        tr.debrisPrefab = (GameObject)EditorGUILayout.ObjectField(tr.debrisPrefab, typeof(GameObject), true);
        tr.radius = EditorGUILayout.FloatField("Radius", tr.radius);
        tr.amount = EditorGUILayout.IntField("Debris Amount", tr.amount);

        if (GUILayout.Button("Create Debris"))
        {
            tr.CreateDebris();
            //(on)? !on : !on;
            if (debris) { debris = false; } else { debris = true; }
        }
        if (GUILayout.Button("Destroy Lost Debris"))
        {
            tr.DestroyLostDebris();
        }
        //Create Inspector elements for each debris instance
        if (tr.clouds == null) { return; }
        if (debris || tr.clouds.Length > 0)
        {
            for (int i = 0; i < tr.clouds.Length; ++i)
            {
                if (tr.clouds[i].debris != null)
                {
                    EditorGUILayout.LabelField("Debris #" + (i + 1));
                    tr.clouds[i].locatDeg = EditorGUILayout.IntSlider("Relative Position ", tr.clouds[i].locatDeg, 0, 360);
                    tr.clouds[i].debris.transform.position = Helper.CalculateDegPos(tr.clouds[i].locatDeg, tr.clouds[i].radius);
                    tr.clouds[i].radius = EditorGUILayout.FloatField("Radius: ", tr.clouds[i].radius);
                    tr.clouds[i].range = EditorGUILayout.IntSlider("Relative Range", tr.clouds[i].range, 0, 360);
                    tr.clouds[i].relRange = new Vector2(tr.clouds[i].locatDeg - tr.clouds[i].range / 2, tr.clouds[i].locatDeg + tr.clouds[i].range / 2);
                    tr.clouds[i].cloudSize = EditorGUILayout.IntField("Cloud Size", tr.clouds[i].cloudSize);
                    if (GUILayout.Button("Create Cloud"))
                    {
                        tr.clouds[i].CreateDebrisCloud();
                        if (cloud) { cloud = !cloud; } else { cloud = !cloud; }
                    }
                    if (GUILayout.Button("Destroy Cloud"))
                    {
                        tr.clouds[i].DestroyDebrisCloud();
                        if (cloud) { cloud = !cloud; } else { cloud = !cloud; }
                    }
                    if (cloud || tr.clouds[i].debrisCloud != null)
                    {
                        tr.clouds[i].UpdatePos();
                    }
                }
            }
        }
    }
}