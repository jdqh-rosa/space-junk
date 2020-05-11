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
            clouds[i].locatDeg = (360/amount)*i;
            Vector2 blah = CalculateDegPos((360/amount)*i, radius);
            clouds[i].debris = Instantiate(debrisPrefab,
            new Vector3(blah.x, blah.y ,0),
            Quaternion.identity) as GameObject;
            
            clouds[i].debris.transform.parent = transform;
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
        tr.radius = EditorGUILayout.FloatField("Radius", tr.radius);
        tr.amount = EditorGUILayout.IntField("Debris Amount", tr.amount);
        tr.debrisPrefab = (GameObject)EditorGUILayout.ObjectField(tr.debrisPrefab, typeof(GameObject), true);
        
        if(GUILayout.Button("Create Debris")){
            tr.CreateDebris();
            //(on)? !on : !on;
            if(on){on=false;}else{on=true;}
        }

        if(on || tr.clouds.Length>0){
            for(int i=0; i<tr.clouds.Length;++i){
            tr.clouds[i].locatDeg = EditorGUILayout.IntSlider("Relative Position " + (i+1),tr.clouds[i].locatDeg, 0,360);
            tr.clouds[i].debris.transform.position = tr.CalculateDegPos(tr.clouds[i].locatDeg, tr.radius);
            }
        }       
    }
}