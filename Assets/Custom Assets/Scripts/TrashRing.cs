using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class TrashRing : MonoBehaviour
{
    public Vector3 pivotPoint;
    public float radius;
    public int amount;
    public GameObject debrisPrefab;
    public TrashCloud[] clouds;  
    public int debrisLocation;

    public void CreateDebris(){
        if(clouds!=null) DestroyDebris();
        clouds = new TrashCloud[amount];
        for(int i=0; i< amount; ++i){
            clouds[i].radius = radius;
            clouds[i].locatDeg = (360/amount)*i;
            Vector2 blah = CalculateDegPos((360/amount)*i, radius);
            clouds[i].debris = Instantiate(debrisPrefab,
            new Vector3(blah.x, blah.y ,0),
            Quaternion.identity) as GameObject;
            
            clouds[i].debris.transform.parent = transform;
        }
    }

    public void CreateCloud(TrashCloud cloud){
        cloud.debrisCloud = new GameObject[cloud.cloudSize];
        for(int i=0; i< cloud.cloudSize; ++i){
            Vector2 blah = CalculateDegPos(cloud.relRange.x + (cloud.range/amount)*i, cloud.radius);
            cloud.debrisCloud[i] = Instantiate(cloud.debris,
            new Vector3(blah.x, blah.y, 0),
            Quaternion.identity) as GameObject;
        }
    }

    void DestroyDebris(){
        for(int i=0; i< clouds.Length; ++i){
            DestroyImmediate(clouds[i].debris, true);
        }
        clouds = null;
    }

    public float CalculateCirc(float pRadius){
        return (2 * Mathf.PI * pRadius);
    }

    public float CalculateCircDeg(float pRadius){
        return 360/CalculateCirc(pRadius);
    }

    public Vector2 CalculateDegPos(float deg, float pRadius)
    {
        float x = pRadius * Mathf.Cos(deg*Mathf.Deg2Rad);
        float y = x * Mathf.Tan(deg*Mathf.Deg2Rad);
        return new Vector2(x,y);
    }
}


[CustomEditor(typeof(TrashRing))]
public class TrashRingEditor : Editor
{
    bool on=false;
    public override void OnInspectorGUI(){
        serializedObject.Update();
        
        TrashRing tr = (TrashRing)target;

        //DrawDefaultInspector();

        tr.pivotPoint = EditorGUILayout.Vector3Field("Pivot Point", tr.pivotPoint);
        tr.debrisPrefab = (GameObject)EditorGUILayout.ObjectField(tr.debrisPrefab, typeof(GameObject), true);
        tr.radius = EditorGUILayout.FloatField("Radius", tr.radius);
        tr.amount = EditorGUILayout.IntField("Debris Amount", tr.amount);
        
        if(GUILayout.Button("Create Debris")){
            tr.CreateDebris();
            //(on)? !on : !on;
            if(on){on=false;}else{on=true;}
        }

        if(on || tr.clouds.Length>0){
            for(int i=0; i<tr.clouds.Length;++i){
                EditorGUILayout.LabelField("Debris #" + (i+1));
                tr.clouds[i].locatDeg = EditorGUILayout.IntSlider("Relative Position ",tr.clouds[i].locatDeg, 0,360);
                tr.clouds[i].debris.transform.position = tr.CalculateDegPos(tr.clouds[i].locatDeg, tr.clouds[i].radius);
                tr.clouds[i].radius = EditorGUILayout.FloatField("Radius: ", tr.clouds[i].radius);
                tr.clouds[i].range = EditorGUILayout.IntSlider("Relative Range", tr.clouds[i].range, 0, 360);
                tr.clouds[i].relRange = new Vector2(tr.clouds[i].locatDeg - tr.clouds[i].range/2, tr.clouds[i].locatDeg + tr.clouds[i].range/2);
                tr.clouds[i].cloudSize = EditorGUILayout.IntField("Cloud Size", tr.clouds[i].cloudSize);
                if(GUILayout.Button("Create Cloud")){
                    tr.CreateCloud(tr.clouds[i]);
                }
            }
        }       
    }
}