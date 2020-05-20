using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//**// GAME MANAGER CODE //**//
//**// NFI //**//

[ExecuteInEditMode]
public class GameManager : MonoBehaviour
{
    public GameObject earth;
    public GameObject satellite;
    public GameObject earthBase;
    public GameObject rocket;
    public GameObject spaceDebris;
    public TrashRing trashRing;
    
    public bool init=false;

    //public void Init()
    //{
    //    earths = new GameObject[3];
    //    satellites = new GameObject[3];
    //    earthBases = new GameObject[3];
    //    rockets = new GameObject[3];
    //    spaceDebris = new GameObject[3];
    //    trashRing = new TrashRing();
    //    init =true;
    //}

    void Update()
    {

    }
}

enum Phase
{
    Past,
    Present,
    Future
}

//**// GAME MANAGER INSPECTOR CODE //**//
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    bool debris;
    bool cloud;
    bool showPastObjects;
    bool showPresentObjects;
    bool showFutureObjects;
    bool showPrefabs;

    public override void OnInspectorGUI(){
        serializedObject.Update();
        
        GameManager gm = (GameManager)target;
        if(gm.trashRing==null){gm.trashRing = new TrashRing();}
        TrashRing tr = gm.trashRing;

//GameObjects

        showPrefabs = EditorGUILayout.Foldout(showPrefabs, "Game Prefabs");
        if (showPrefabs)
        {
            gm.earth = (GameObject)EditorGUILayout.ObjectField("Earth", gm.earth, typeof(GameObject), true);
            gm.satellite = (GameObject)EditorGUILayout.ObjectField("Satellite", gm.satellite, typeof(GameObject), true);
            gm.earthBase = (GameObject)EditorGUILayout.ObjectField("Base", gm.earthBase, typeof(GameObject), true);
            gm.rocket = (GameObject)EditorGUILayout.ObjectField("Rocket", gm.rocket, typeof(GameObject), true);
            gm.spaceDebris = (GameObject)EditorGUILayout.ObjectField("Space Debris", gm.spaceDebris, typeof(GameObject), true);
        }

        //if(!gm.init){gm.Init();}

        //showPastObjects = EditorGUILayout.Foldout(showPastObjects, "Game Objects Past");
        //if(showPastObjects)
        //{
        //    gm.earths[(int)Phase.Past] = (GameObject)EditorGUILayout.ObjectField("Earth", gm.earths[(int)Phase.Past], typeof(GameObject), true);
        //    gm.satellites[(int)Phase.Past] = (GameObject)EditorGUILayout.ObjectField("Satellite", gm.satellites[(int)Phase.Past], typeof(GameObject), true);
        //    gm.earthBases[(int)Phase.Past] = (GameObject)EditorGUILayout.ObjectField("Base", gm.earthBases[(int)Phase.Past], typeof(GameObject), true);
        //    gm.rockets[(int)Phase.Past] = (GameObject)EditorGUILayout.ObjectField("Rocket", gm.rockets[(int)Phase.Past], typeof(GameObject), true);
        //    gm.spaceDebris[(int)Phase.Past] = (GameObject)EditorGUILayout.ObjectField("Space Debris", gm.spaceDebris[(int)Phase.Past], typeof(GameObject), true);
        //}
        //showPresentObjects = EditorGUILayout.Foldout(showPresentObjects, "Game Objects Present");
        //if(showPresentObjects)
        //{
        //    gm.earths[(int)Phase.Present] = (GameObject)EditorGUILayout.ObjectField("Earth", gm.earths[(int)Phase.Present], typeof(GameObject), true);
        //    gm.satellites[(int)Phase.Present] = (GameObject)EditorGUILayout.ObjectField("Satellite", gm.satellites[(int)Phase.Present], typeof(GameObject), true);
        //    gm.earthBases[(int)Phase.Present] = (GameObject)EditorGUILayout.ObjectField("Base", gm.earthBases[(int)Phase.Present], typeof(GameObject), true);
        //    gm.rockets[(int)Phase.Present] = (GameObject)EditorGUILayout.ObjectField("Rocket", gm.rockets[(int)Phase.Present], typeof(GameObject), true);
        //    gm.spaceDebris[(int)Phase.Present] = (GameObject)EditorGUILayout.ObjectField("Space Debris", gm.spaceDebris[(int)Phase.Present], typeof(GameObject), true);
        //}
        //showFutureObjects = EditorGUILayout.Foldout(showFutureObjects, "Game Objects Future");
        //if(showFutureObjects)
        //{
        //    gm.earths[(int)Phase.Future] = (GameObject)EditorGUILayout.ObjectField("Earth", gm.earths[(int)Phase.Future], typeof(GameObject), true);
        //    gm.satellites[(int)Phase.Future] = (GameObject)EditorGUILayout.ObjectField("Satellite", gm.satellites[(int)Phase.Future], typeof(GameObject), true);
        //    gm.earthBases[(int)Phase.Future] = (GameObject)EditorGUILayout.ObjectField("Base", gm.earthBases[(int)Phase.Future], typeof(GameObject), true);
        //    gm.rockets[(int)Phase.Future] = (GameObject)EditorGUILayout.ObjectField("Rocket", gm.rockets[(int)Phase.Future], typeof(GameObject), true);
        //    gm.spaceDebris[(int)Phase.Future] = (GameObject)EditorGUILayout.ObjectField("Space Debris", gm.spaceDebris[(int)Phase.Future], typeof(GameObject), true);
        //}


        tr.pivotPoint = EditorGUILayout.Vector3Field("Pivot Point", tr.pivotPoint);
        tr.debrisPrefab = (GameObject)EditorGUILayout.ObjectField(tr.debrisPrefab, typeof(GameObject), true);
        tr.radius = EditorGUILayout.FloatField("Radius", tr.radius);
        tr.amount = EditorGUILayout.IntField("Debris Amount", tr.amount);
        
        if(GUILayout.Button("Create Debris")){
            tr.CreateDebris();
            //(on)? !on : !on;
            if(debris){debris=false;}else{debris=true;}
        }
//Create Inspector elements for each debris instance
        if(tr.clouds==null){return;}
        if(debris || tr.clouds.Length>0){
            for(int i=0; i<tr.clouds.Length;++i){
                EditorGUILayout.LabelField("Debris #" + (i+1));
                gm.trashRing.clouds[i].locatDeg = EditorGUILayout.IntSlider("Relative Position ", gm.trashRing.clouds[i].locatDeg, 0,360);
                gm.trashRing.clouds[i].debris.transform.position = Helper.CalculateDegPos(tr.clouds[i].locatDeg, tr.clouds[i].radius);
                gm.trashRing.clouds[i].radius = EditorGUILayout.FloatField("Radius: ", tr.clouds[i].radius);
                gm.trashRing.clouds[i].range = EditorGUILayout.IntSlider("Relative Range", tr.clouds[i].range, 0, 360);
                gm.trashRing.clouds[i].relRange = new Vector2(tr.clouds[i].locatDeg - tr.clouds[i].range/2, tr.clouds[i].locatDeg + tr.clouds[i].range/2);
                gm.trashRing.clouds[i].cloudSize = EditorGUILayout.IntField("Cloud Size", tr.clouds[i].cloudSize);
                if(GUILayout.Button("Create Cloud")){
                    tr.clouds[i].CreateDebrisCloud();
                    //tr.InstantiateCloud();
                    if(cloud){cloud=!cloud;}else{cloud=!cloud;}
                }
                if(GUILayout.Button("Destroy Cloud")){
                    tr.clouds[i].DestroyDebrisCloud();
                    if(cloud){cloud=!cloud;}else{cloud=!cloud;}
                }
                if(cloud || tr.clouds[i].debrisCloud!=null)
                {
                    tr.clouds[i].UpdatePos();
                }
            }
        } 
    }
}
