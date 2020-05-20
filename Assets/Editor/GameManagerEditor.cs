using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//**// GAME MANAGER INSPECTOR CODE //**//
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    bool debris;
    bool cloud;
    bool showPastObjects = false;
    bool showPresentObjects = false;
    bool showFutureObjects = false;
    bool showPrefabs;

    public override void OnInspectorGUI(){
        serializedObject.Update();
        
        base.OnInspectorGUI();

        GameManager gm = (GameManager)target;
        if(gm.trashRing==null){gm.trashRing = new TrashRing();}
        TrashRing tr = gm.trashRing;

        if (!gm.init) { gm.Init(); }

        //showPastObjects = EditorGUILayout.Foldout(showPastObjects, "Past");
        //if (showPastObjects)
        //{
        //    gm.earths[(int)Phase.Past] = (GameObject)EditorGUILayout.ObjectField("Earth", gm.earths[(int)Phase.Past], typeof(GameObject), true);
        //    gm.satellites[(int)Phase.Past] = (GameObject)EditorGUILayout.ObjectField("Satellite", gm.satellites[(int)Phase.Past], typeof(GameObject), true);
        //    gm.earthBases[(int)Phase.Past] = (GameObject)EditorGUILayout.ObjectField("Base", gm.earthBases[(int)Phase.Past], typeof(GameObject), true);
        //    gm.rockets[(int)Phase.Past] = (GameObject)EditorGUILayout.ObjectField("Rocket", gm.rockets[(int)Phase.Past], typeof(GameObject), true);
        //    gm.spaceDebris[(int)Phase.Past] = (GameObject)EditorGUILayout.ObjectField("Space Debris", gm.spaceDebris[(int)Phase.Past], typeof(GameObject), true);
        //}
        //showPresentObjects = EditorGUILayout.Foldout(showPresentObjects, "Present");
        //if (showPresentObjects)
        //{
        //    gm.earths[(int)Phase.Present] = (GameObject)EditorGUILayout.ObjectField("Earth", gm.earths[(int)Phase.Present], typeof(GameObject), true);
        //    gm.satellites[(int)Phase.Present] = (GameObject)EditorGUILayout.ObjectField("Satellite", gm.satellites[(int)Phase.Present], typeof(GameObject), true);
        //    gm.earthBases[(int)Phase.Present] = (GameObject)EditorGUILayout.ObjectField("Base", gm.earthBases[(int)Phase.Present], typeof(GameObject), true);
        //    gm.rockets[(int)Phase.Present] = (GameObject)EditorGUILayout.ObjectField("Rocket", gm.rockets[(int)Phase.Present], typeof(GameObject), true);
        //    gm.spaceDebris[(int)Phase.Present] = (GameObject)EditorGUILayout.ObjectField("Space Debris", gm.spaceDebris[(int)Phase.Present], typeof(GameObject), true);
        //}
        //showFutureObjects = EditorGUILayout.Foldout(showFutureObjects, "Future");
        //if (showFutureObjects)
        //{
        //    gm.earths[(int)Phase.Future] = (GameObject)EditorGUILayout.ObjectField("Earth", gm.earths[(int)Phase.Future], typeof(GameObject), true);
        //    gm.satellites[(int)Phase.Future] = (GameObject)EditorGUILayout.ObjectField("Satellite", gm.satellites[(int)Phase.Future], typeof(GameObject), true);
        //    gm.earthBases[(int)Phase.Future] = (GameObject)EditorGUILayout.ObjectField("Base", gm.earthBases[(int)Phase.Future], typeof(GameObject), true);
        //    gm.rockets[(int)Phase.Future] = (GameObject)EditorGUILayout.ObjectField("Rocket", gm.rockets[(int)Phase.Future], typeof(GameObject), true);
        //    gm.spaceDebris[(int)Phase.Future] = (GameObject)EditorGUILayout.ObjectField("Space Debris", gm.spaceDebris[(int)Phase.Future], typeof(GameObject), true);
        //}


        tr.pivotPoint = EditorGUILayout.Vector3Field("Pivot Point", tr.pivotPoint);
        tr.debrisPrefab = (GameObject)EditorGUILayout.ObjectField("Debris Prefab", tr.debrisPrefab, typeof(GameObject), true);
        tr.radius = EditorGUILayout.FloatField("Radius", tr.radius);
        tr.amount = EditorGUILayout.IntField("Debris Amount", tr.amount);
        
        if(GUILayout.Button("Create Debris")){
            tr.CreateDebris();
            if(debris){debris=false;}else{debris=true;}
        }

        if(GUILayout.Button("Destroy Lost Debris")){
            tr.DestroyLostDebris();
            if(debris){debris=false;}
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

